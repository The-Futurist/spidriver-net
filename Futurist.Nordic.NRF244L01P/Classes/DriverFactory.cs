using Radio.Nordic.NRF24L01P.Drivers;

// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public static class DriverFactory
    {
        public static IRadioDriver CreateDriver(DriverSettings Settings)
        {
            return Settings switch
            {
                FT230XQSettings => CreateFT230XQDriver((FT230XQSettings)Settings),
                FT232HSettings => CreateFT232HDriver((FT232HSettings)Settings) ,
                 _ => throw new ArgumentException("Unsupported settings class")
            };
        }

        //public static IRadioDriver CreateSPIDriver(string Comport, Output CEPin)
        private static IRadioDriver CreateFT230XQDriver(FT230XQSettings Settings)
        {
            return new FT230XQDriver(Settings.ComPort, Settings.CEPin);
        }

        private static IRadioDriver CreateFT232HDriver(FT232HSettings Settings)
        {
            return new FT232HDriver(Settings.CSPin, Settings.CEPin, Settings.ClockSpeed);
        }
    }

    public abstract class DriverSettings
    {
    }
    public class FT230XQSettings : DriverSettings
    {
        public required string ComPort { get; set; }
        public SpiDriver.Output CEPin { get; set; }
    }

    public class FT232HSettings : DriverSettings
    {
        public required string CSPin { get; set; }
        public required string CEPin { get; set; }
        public int ClockSpeed { get; set; }
    }
}
