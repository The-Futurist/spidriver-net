using Radio.Nordic.NRF24L01P;
using SpiDriver;
using System.Text;
using static Radio.Nordic.NRF24L01P.CRC;

namespace Sandbox
{
    // SEE: https://learn.microsoft.com/en-us/dotnet/iot/usb
    // SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

    class Program
    {
        private static Address nucleo_1_address = new(0x1951383138); // board's ID address
        private static Address nucleo_2_address = new(0x0F50334636); // board's ID address
        private static string text = "I am a test messags of length 32";
        private static Random rand = new Random(); // Create Random instance

        static void Main(string[] argv)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            STATUS sTATUS = new STATUS();

            try
            {
                if (NRF24L01P.TryGetNrfComPort(out var port))
                {
                    using NRF24L01P nrf = new(port, Output.A);

                    nrf.ConnectUSB();
                    nrf.Reset();
                    nrf.ConfigureRadio(Channel: 9, OutputPower.Min, DataRate.Med);
                    nrf.ClearInterruptFlags(true, true, true);
                    nrf.SetPipeState(Pipe.Pipe_0, true);
                    nrf.SetPipeState(Pipe.Pipe_1, false);

                    nrf.SetAutoAck(Pipe.Pipe_0, true);
                    nrf.SetTransmitMode();
                    nrf.SetCRC(true, TwoBytes);
                    nrf.SetAddressWidth(5);
                    nrf.SetAutoAckRetries(Interval: 1, MaxRetries: 10);

                    // Variable length payloads over pipe 0 (the boards listening address)

                    nrf.SetDynamicPayload(true);
                    nrf.SetDynamicPayloadPipe(Pipe.Pipe_0, true);
                    nrf.SetDynamicAck(true);
                    nrf.PowerUp();

                    while (true)
                    {
                        //Send random length messsage to first board

                        SendMessage(nrf, nucleo_1_address, message);

                        Thread.Sleep(10);

                        //Send random length messsage to second board

                        SendMessage(nrf, nucleo_2_address, message);

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

        private static void SendMessage(NRF24L01P Nrf, Address Address, byte[] Message)
        {
            Nrf.SetReceiveAddressLong(Address, Pipe.Pipe_0);
            Nrf.SetTransmitAddress(Address);

            int randsize = rand.Next(1, 33);

            Nrf.SendPayload(Message, randsize, true);

            var status = Nrf.PollStatusUntil(s => s.MAX_RT | s.TX_DS);

            if (status.MAX_RT)
            {
                Nrf.FlushTransmitFifo();
                LogFailedAck(Nrf, Address);
            }

            Nrf.ClearInterruptFlags(true, true, true);
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