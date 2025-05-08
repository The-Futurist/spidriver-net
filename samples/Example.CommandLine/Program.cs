using Radio.Nordic;

namespace Example.CommandLine
{
    unsafe class Program
    {
        static void Main(string[] argv)
        {
            NRF24L01P nrf = new NRF24L01P("COM7");

            nrf.ConnectUSB();

            nrf.CS = Pin.High;
            nrf.CE = Pin.Low;

            var config = nrf.ReadRegister<CONFIG>();
            var en_aa = nrf.ReadRegister<EN_AA>();

            en_aa.ENAA_P1 = false;

            nrf.WriteRegister(en_aa);

            en_aa = nrf.ReadRegister<EN_AA>();

            var rx_addr_p0 = nrf.ReadRegister<RX_ADDR_P0>();
            var rx_addr_p1 = nrf.ReadRegister<RX_ADDR_P1>();
        }


    }
}
