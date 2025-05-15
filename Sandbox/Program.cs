using Radio.Nordic.NRF24L01P;

namespace Sandbox
{
    class Program
    {
        private static ulong address = 0x19513831AA; // Testing address
        private static byte[] payload = { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 };

        static void Main(string[] argv)
        {
            var port = NRF24L01P.GetNrfComPort();

            using (NRF24L01P nrf = new NRF24L01P(port))
            {
                nrf.ConnectUSB();
                nrf.Reset();
                nrf.ConfigureRadio(9, 1, 0);
                nrf.ClearInterruptFlags(true,true,true);
                nrf.SetPipeState(Pipe.Pipe_0, true);
                nrf.SetPipeState(Pipe.Pipe_1, false);

                nrf.SetAutoAck(Pipe.Pipe_0,true);
                nrf.SetTransmitMode();
                nrf.SetCRC(true,true);
                nrf.SetAddressWidth(3);
                nrf.SetAutoAckRetries(1, 10);
                nrf.PowerUp();

                nrf.SetReceiveAddressLong(address, Pipe.Pipe_0);
                nrf.SetTransmitAddress(address);

                STATUS status;

                status = nrf.ReadRegister<STATUS>();

                while (true)
                {
                    nrf.SendPayload(payload);
                    Thread.Sleep(20);
                    status = nrf.ReadRegister<STATUS>();
                }
            }
        }
    }
}
