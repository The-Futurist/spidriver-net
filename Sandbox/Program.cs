using Radio.Nordic.NRF24L01P;
using static Radio.Nordic.NRF24L01P.CRC;
using static Sandbox.Boards;
using static Radio.Nordic.NRF24L01P.Direction;

namespace Sandbox
{
    // SEE: https://learn.microsoft.com/en-us/dotnet/iot/usb
    // SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

    class Program
    {
        private static Address[] boards = { new(NUCLEO_1), new(NUCLEO_3) };//, new(NUCLEO_3) };
        private static string text = "I am a test messags of length 32";
        private static Random rand = new Random(); // Create Random instance
        private static int msgCount = 0;
        static void Main(string[] argv)
        {
            byte[] message = { 0xAB, 0xCD };

            try
            {
                //if (NRF24L01P.TryGetNrfComPort(out var port))
                {
                    //Console.WriteLine($"Using Port: {port}.");

                    var settings = new FT232HSettings() { CSPin = "D3", CEPin = "D4", ClockSpeed = 10_000_000 };

                    using NRF24L01P radio = NRF24L01P.Create(settings);

                    radio.Connect();
                    radio.Reset();
                    radio.ConfigureRadio(Channel: 9, OutputPower.Min, DataRate.Med);
                    radio.ClearInterruptFlags(true, true, true);
                    radio.SetPipeState(Pipe.Pipe_0, true);
                    radio.SetPipeState(Pipe.Pipe_1, false);

                    radio.SetAutoAck(Pipe.Pipe_0, true);
                    radio.SetDirection(Transmit);
                    radio.SetCRC(true, TwoBytes);
                    radio.SetAddressWidth(5);
                    radio.SetAutoAckRetries(Interval: 1, MaxRetries: 10);

                    // Variable length payloads over pipe 0 (the boards listening address)

                    radio.SetDynamicPayload(true);
                    radio.SetDynamicPayloadPipe(Pipe.Pipe_0, true);
                    radio.SetDynamicAck(true);
                    radio.PowerUp();

                    while (true)
                    {
                        foreach (var board in boards)
                        {
                            SendMessage(radio, board, message);
                            Thread.Sleep(10);
                        }
                    }
                }
                //else
                //{
                //    Console.WriteLine("No NRF Device COM Port was found on this computer");
                //}
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

        private static void SendMessage(NRF24L01P Radio, Address Address, byte[] Message)
        {
            Radio.SetReceiveAddressLong(Address, Pipe.Pipe_0);
            Radio.SetTransmitAddress(Address);

            int size = Message.Length;

            Radio.SendPayload(Message, size, true);

            var status = Radio.PollStatusUntil(s => s.MAX_RT | s.TX_DS);

            if (status.MAX_RT)
            {
                Radio.FlushTransmitFifo();
                LogFailedAck(Radio, Address);
            }
            else
            {
                msgCount++;
            }

            if (msgCount > 999)
            {
                LogSuccess(msgCount);
                msgCount = 0;
            }

            Radio.ClearInterruptFlags(true, true, true);
        }

        private static void LogSuccess(int Num)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{DateTime.Now} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Num} messages sent and acknowledged.");
        }
        private static void LogFailedAck(NRF24L01P nrf, Address addr)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{DateTime.Now} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" No auto-ack received from STN: {addr} CHAN: {nrf.Channel} FREQ: {2400 + nrf.Channel} RET: {nrf.Retries} INT: {nrf.Interval} TOT: {(nrf.Retries + 1) * nrf.Interval}");
        }
    }
}