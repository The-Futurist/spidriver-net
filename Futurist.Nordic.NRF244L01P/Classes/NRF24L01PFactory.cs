using SpiDriver;

// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public static class NRF24L01PFactory
    {
        public static INRF24L01IO CreateSPIDriverIO(string Comport, Output CEPin)
        {
            var driver = new SPIDriverIO(Comport, CEPin);
            return driver;
        }

    }
}