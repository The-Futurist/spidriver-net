using Radio.Nordic.NRF24L01P.Drivers;
using SpiDriver;

// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public static class DriverFactory
    {
        public static IRadioDriver CreateDriver(DriverSettings Settings)
        {
            return Settings switch
            {
                FT230XQSettings => CreateSPIDriver((FT230XQSettings)Settings),
                FT232HSettings => CreateFT232H((FT232HSettings)Settings) ,
                 _ => throw new ArgumentException("Unsupported settings class")
            };
        }

        //public static IRadioDriver CreateSPIDriver(string Comport, Output CEPin)
        private static IRadioDriver CreateSPIDriver(FT230XQSettings Settings)
        {
            var driver = new FT230XQDriver(Settings.ComPort, Settings.CEPin);
            return driver;
        }

        private static IRadioDriver CreateFT232H(FT232HSettings Settings)
        {
            var driver = new FT232HDriver(Settings.CSPin, Settings.CEPin, Settings.ClockSpeed);
            return driver;
        }
    }

    public abstract class DriverSettings
    {
    }
    public class FT230XQSettings : DriverSettings
    {
        public string ComPort { get; set; }
        public Output CEPin { get; set; }
    }

    public class FT232HSettings : DriverSettings
    {
        public string CSPin { get; set; }
        public string CEPin { get; set; }
        public int ClockSpeed { get; set; }
    }
}
