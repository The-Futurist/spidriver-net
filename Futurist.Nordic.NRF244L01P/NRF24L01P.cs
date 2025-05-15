using SpiDriver;
using System.Management;

// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public class NRF24L01P : IDisposable
    {
        private Device device;
        private bool disposedValue;
        public NRF24L01P(string comport)
        {
            this.device = new Device(comport);
        }
        public void ConnectUSB()
        {
            device.Connect();
            CS = Pin.High;
            CE = Pin.Low;
        }
        public static string GetNrfComPort()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'"))
            {
                foreach (var obj in searcher.Get())
                {
                    if (obj["Manufacturer"].ToString() == "FTDI")
                    {
                        var s = obj["Name"].ToString().Split(new char[] { '(', ')' }, 10);
                        return s[1];
                    }
                }
            }

            return null;
        }
        public T ReadRegister<T>() where T : REGISTER, new()
        {
            T register = new T();
            SetCSLow();
            device.Write(new byte[] { (byte)((byte)COMMNAND.R_REGISTER | register.Id) }, 0, 1);
            device.Read(register.Register, 0, register.Length);
            SetCSHigh();
            return register;
        }
        public void WriteRegister<T>(T register) where T : REGISTER
        {
            SetCSLow();
            device.Write(new byte[] { (byte)((byte)COMMNAND.W_REGISTER | register.Id) }, 0, 1);
            device.Write(register.Register, 0, register.Length);
            SetCSHigh();
        }
        public Pin CS
        {
            set => device.SetOutput(Output.CS, value == Pin.Low ? true : false);
        }
        public Pin CE
        {
            set => device.SetOutput(Output.CS, value == Pin.Low ? false : true);
        }
        public void SetCSLow()
        {
            device.SetOutput(Output.CS, true);
        }
        public void SetCSHigh()
        {
            device.SetOutput(Output.CS, false);
        }
        public void SetCELow()
        {
            device.SetOutput(Output.A, false);
        }
        public void SetCEHigh()
        {
            device.SetOutput(Output.A, true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                device.Close();
                //device.Dispose();
                disposedValue = true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~NRF24L01P()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}