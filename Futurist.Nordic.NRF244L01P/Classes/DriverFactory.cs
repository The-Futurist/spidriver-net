using Radio.Nordic.NRF24L01P.Drivers;
using SpiDriver;

// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public static class DriverFactory
    {
        public static IRadioDriver CreateSPIDriver(string Comport, Output CEPin)
        {
            var driver = new FT230XQDriver(Comport, CEPin);
            return driver;
        }

        public static IRadioDriver CreateFT232H(string CSPin, string CEPin)
        {
            var driver = new FT232HDriver(CSPin, CEPin);
            return driver;
        }
    }
}