using Futurist.Nordic.NRF244L01P;
using Radio.Nordic.NRF24L01P;
using SpiDriver;

namespace Sandbox
{
    // SEE: https://learn.microsoft.com/en-us/dotnet/iot/usb

    class Program
    {
        private static Address nucleo_1_address = new(0x1951383138); // board's ID address
        private static Address nucleo_2_address = new(0x0F50334636); // board's ID address
        private static byte[] payload = [0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88];

        static void Main(string[] argv)
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
                nrf.SetAutoAckRetries(Interval: 1, MaxRetries: 1);
                nrf.PowerUp();

                while (true)
                {
                    nrf.SetReceiveAddressLong(nucleo_1_address, Pipe.Pipe_0);
                    nrf.SetTransmitAddress(nucleo_1_address);
                    nrf.SendPayload(payload);

                    var status = nrf.PollStatusUntil(s => s.MAX_RT == true | s.TX_DS == true);

                    if (status.MAX_RT)
                    {
                        LogFailedAck(nrf, nucleo_1_address);
                    }

                    nrf.ClearInterruptFlags(true, true, true);

                    Thread.Sleep(50);

                    nrf.SetReceiveAddressLong(nucleo_2_address, Pipe.Pipe_0);
                    nrf.SetTransmitAddress(nucleo_2_address);
                    nrf.SendPayload(payload);

                    status = nrf.PollStatusUntil(s => s.MAX_RT == true | s.TX_DS == true);

                    if (status.MAX_RT)
                    {
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
            Console.WriteLine($" No auto-ack received. STN: {addr} CHAN: {nrf.Channel} FREQ: {2400 + nrf.Channel} RET: {nrf.Retries} INT: {nrf.Interval} TOT: {(nrf.Retries + 1) * nrf.Interval}");
        }
    }
}