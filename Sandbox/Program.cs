using Radio.Nordic.NRF24L01P;
using static Radio.Nordic.NRF24L01P.CRC;
using static Sandbox.Boards;
using static Radio.Nordic.NRF24L01P.Direction;
using System.Diagnostics;

namespace Sandbox
{
    // SEE: https://learn.microsoft.com/en-us/dotnet/iot/usb
    // SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

    class Program
    {
        private static readonly Address[] remote_boards = [new(NUCLEO_1), new(NUCLEO_3)];//, new(NUCLEO_3) };
        private static readonly Random random = new();
        private static int msgCount = 0;

        static void Main(string[] argv)
        {
            byte[] message = [0xAB, 0xCD, 0xEF,0x4A, 0x66, 0x8B, 0x4C];

            try
            {
                using var radio = NRF24L01P.Create(new FT232HSettings() { CSNPin = "D3", CENPin = "D4", IRQPin = "D5", ClockSpeed = 10_000_000 });

                radio.Reset();
                radio.ConfigureRadio(Channel: 9, OutputPower.Min, DataRate.Med);
                radio.ClearInterruptFlags(true, true, true);
                radio.SetPipeState(Pipe.Pipe_0, true);
                radio.SetPipeState(Pipe.Pipe_1, false);
                radio.SetAutoAck(Pipe.Pipe_0, true);
                radio.WorkingMode = Receive;
                radio.SetCRC(true, TwoBytes);
                radio.AddressWidth = 5;    // must be either 3 or 5 no other values allowed
                radio.SetAutoAckRetries(Interval: 1, MaxRetries: 4);

                // Variable length payloads over pipe 0 (the boards listening address)

                radio.DynamicPayloads = true;
                radio.SetDynamicPayloadPipe(Pipe.Pipe_0, true);
                radio.DynamicAck = true;
                radio.PowerUp();

                while (true)
                {
                    foreach (var board in remote_boards)
                    {
                        SendMessage(radio, board, message);
                        Thread.Sleep(1);
                    }
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
        private static void SendMessage(NRF24L01P Radio, Address Address, byte[] Message)
        {
            Radio.SetReceiveAddressLong(Address, Pipe.Pipe_0);

            Radio.TransmitAddress = Address;

            Radio.WorkingMode = Transmit;

            Radio.SendPayload(Message, Message.Length, true);

            Radio.PollInterruptUntil(Pin.Low, out var status);

            Radio.WorkingMode = Receive;

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