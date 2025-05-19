using Futurist.Nordic.NRF244L01P;
using Radio.Nordic.NRF24L01P;
using SpiDriver;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Sandbox
{
    // SEE: https://learn.microsoft.com/en-us/dotnet/iot/usb
    // SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

    class Program
    {
        private static Address nucleo_1_address = new(0x1951383138); // board's ID address
        private static Address nucleo_2_address = new(0x0F50334636); // board's ID address
        private static byte[] payload = [0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88]; //, 0xe3, 0x41, 0x7f];
        private static ulong counter = 0;

        static void Main(string[] argv)
        {
            try
            {
                if (NRF24L01P.TryGetNrfComPort(out var port))
                {
                    using NRF24L01P nrf = new(port, Output.A);

                    nrf.ConnectUSB();
                    nrf.Reset();
                    nrf.ConfigureRadio(Channel: 9, OutputPower.Low, DataRate.Med);
                    nrf.ClearInterruptFlags(true, true, true);
                    nrf.SetPipeState(Pipe.Pipe_0, true);
                    nrf.SetPipeState(Pipe.Pipe_1, false);

                    nrf.SetAutoAck(Pipe.Pipe_0, true);
                    nrf.SetTransmitMode();
                    nrf.SetCRC(true, true);
                    nrf.SetAddressWidth(5);
                    nrf.SetAutoAckRetries(Interval: 3, MaxRetries: 5);   // this might be the cause of the receiver lockup, if the RX sends an ack
                                                                         // but the TX never gets it, the TX will resend and potetnially re-trigger
                                                                         // an interrupt at the RX while in the middle of processing the earllier 
                                                                         // (received) message. 


                    var ftr = nrf.ReadRegister<FEATURE>();
                    ftr.EN_DPL = true;
                    nrf.WriteRegister(ftr);

                    var dyn = nrf.ReadRegister<DYNPD>();
                    dyn.DPL_P0 = true;
                    nrf.WriteRegister(dyn);


                    nrf.PowerUp();

                    while (true)
                    {
                        // Create a new distinct payload for testing purposes

                        byte[] msg = BitConverter.GetBytes(counter++);
                        Array.Reverse(msg);

                        // Send short messsage to first board

                        nrf.SetReceiveAddressLong(nucleo_1_address, Pipe.Pipe_0);
                        nrf.SetTransmitAddress(nucleo_1_address);
                        nrf.SendPayload(msg);

                        var status = nrf.PollStatusUntil(s => s.MAX_RT == true | s.TX_DS == true);

                        if (status.MAX_RT)
                        {
                            nrf.FlushTransmitBuffer();
                            LogFailedAck(nrf, nucleo_1_address);
                        }

                        nrf.ClearInterruptFlags(true, true, true);

                        Thread.Sleep(50);

                        // Send short messsage to second board

                        nrf.SetReceiveAddressLong(nucleo_2_address, Pipe.Pipe_0);
                        nrf.SetTransmitAddress(nucleo_2_address);
                        nrf.SendPayload(msg);

                        status = nrf.PollStatusUntil(s => s.MAX_RT == true | s.TX_DS == true);

                        if (status.MAX_RT)
                        {
                            nrf.FlushTransmitBuffer();
                            LogFailedAck(nrf, nucleo_2_address);
                        }

                        nrf.ClearInterruptFlags(true, true, true);

                        Thread.Sleep(50);
                    }
                }
                else
                {
                    Console.WriteLine("No NRF Device Port was found on this computer");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

        }

        private static string GetAddressText(ulong Address)
        {
            string hexString = Address.ToString("X10");
            return ($"{hexString[..2]}-{hexString[2..4]}-{hexString[4..6]}-{hexString[6..8]}-{hexString[8..10]}");
        }

        private static void LogFailedAck(NRF24L01P nrf, Address addr)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(DateTime.Now);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" No auto-ack received from STN: {addr} CHAN: {nrf.Channel} FREQ: {2400 + nrf.Channel} RET: {nrf.Retries} INT: {nrf.Interval} TOT: {(nrf.Retries + 1) * nrf.Interval}");
        }
    }
}