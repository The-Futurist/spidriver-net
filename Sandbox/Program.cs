using Radio.Nordic.NRF24L01P;
using SpiDriver;
using System.Text;

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
        private static string text = "I am a test messags of length 32";

        static void Main(string[] argv)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            Random rand = new Random(); // Create Random instance

            int randsize = rand.Next(1, 33); // Generates a number from 1 to 32

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
                    nrf.SetAutoAckRetries(Interval: 1, MaxRetries: 10);

                    // Variable length payloads over pipe 0 (the boards listening address)
                    nrf.SetDynamicPayload(true);
                    nrf.SetDynamicPipe(Pipe.Pipe_0, true);

                    nrf.PowerUp();

                    while (true)
                    {
                        // Send short messsage to first board

                        nrf.SetReceiveAddressLong(nucleo_1_address, Pipe.Pipe_0);
                        nrf.SetTransmitAddress(nucleo_1_address);

                        randsize = rand.Next(1, 33);

                        nrf.SendPayload(message, randsize);

                        var status = nrf.PollStatusUntil(s => s.MAX_RT == true | s.TX_DS == true);

                        if (status.MAX_RT)
                        {
                            nrf.FlushTransmitFifo();
                            LogFailedAck(nrf, nucleo_1_address);
                        }

                        nrf.ClearInterruptFlags(true, true, true);

                        Thread.Sleep(10);

                        // Send short messsage to second board

                        nrf.SetReceiveAddressLong(nucleo_2_address, Pipe.Pipe_0);
                        nrf.SetTransmitAddress(nucleo_2_address);

                        randsize = rand.Next(1, 33);

                        nrf.SendPayload(message, randsize);

                        status = nrf.PollStatusUntil(s => s.MAX_RT == true | s.TX_DS == true);

                        if (status.MAX_RT)
                        {
                            nrf.FlushTransmitFifo();
                            LogFailedAck(nrf, nucleo_2_address);
                        }

                        nrf.ClearInterruptFlags(true, true, true);

                        Thread.Sleep(10);
                    }
                }
                else
                {
                    Console.WriteLine("No NRF Device COM Port was found on this computer");
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