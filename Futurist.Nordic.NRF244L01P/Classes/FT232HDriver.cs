// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf
using Iot.Device.Ft232H;
using Iot.Device.FtCommon;
using System.Device.Gpio;
using System.Device.Spi;

namespace Radio.Nordic.NRF24L01P.Drivers
{
    /// <summary>
    /// Implements the driver logic for the FTDI FT232H chip. 
    /// </summary>
    /// <remarks>
    /// This driver supports the Adafruit FT232H Breakout board. 
    /// </remarks>
    public class FT232HDriver: IRadioDriver, IDisposable
    {
        private readonly SpiDevice device;
        private readonly GpioController gpioController;
        private readonly Ft232HDevice ft_device;
        private readonly SpiConnectionSettings settings;
        private readonly int cen_pin;
        private readonly int csn_pin;
        private readonly int irq_pin;
        private bool disposedValue;

        public Pin CSN { set { } }  // the IoT SPI stuff uses CSN implicitly, automatically.
        public Pin CEN 
        {
            set => gpioController?.Write(cen_pin, value == Pin.High ? PinValue.High : PinValue.Low);
        }
        public Pin IRQ
        {
            get => gpioController?.Read(irq_pin) == PinValue.High ? Pin.High : Pin.Low;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CSPin"></param>
        /// <param name="CEPin"></param>
        public FT232HDriver(string CSPin, string CEPin, string IRQPin,int ClockSpeed)
        {
            csn_pin = Ft232HDevice.GetPinNumberFromString(CSPin);
            cen_pin = Ft232HDevice.GetPinNumberFromString(CEPin);
            irq_pin = Ft232HDevice.GetPinNumberFromString(IRQPin);
            settings = new SpiConnectionSettings(0, csn_pin) { ClockFrequency = ClockSpeed, DataBitLength = 8, ChipSelectLineActiveState = PinValue.Low };
            var devices = FtCommon.GetDevices();
            ft_device = new Ft232HDevice(devices[0]);
            gpioController = ft_device.CreateGpioController();
            gpioController.OpenPin(cen_pin, PinMode.Output);
            gpioController.OpenPin(irq_pin, PinMode.Input);

            device = ft_device.CreateSpiDevice(settings);
        }
        public void Close()
        {
            Dispose(true);
        }
        public void Connect()
        {
        }
        public void ReadRegister<T>(out T Register) where T : struct, IRegister
        {
            Register = default;

            if (Register.LENGTH == 1)
            {
                Span<byte> command = [Register.READ, 0xFF];
                Span<byte> response = [0, 0];
                device?.TransferFullDuplex(command, response);
                Register.VALUE = response[1];
            }
            else
            {
                Span<byte> command = [Register.READ, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF];  // 6 bytes
                Span<byte> response = [0, 0, 0, 0, 0, 0];
                device?.TransferFullDuplex(command, response);
                ulong value = 0;
                value |= (ulong)response[1] << 32;
                value |= (ulong)response[2] << 24;
                value |= (ulong)response[3] << 16;
                value |= (ulong)response[4] << 8;
                value |= (ulong)response[5];
                Register.VALUE = value;
            }
        }
        public void SendCommand(byte Command)
        {
            Span<byte> command = [Command];
            Span<byte> response = [0];
            device?.TransferFullDuplex(command, response);
        }
        public void SendCommand(byte Command, Span<byte> Buffer)
        {
            Span<byte> command = stackalloc byte[Buffer.Length + 1]; // Allocate space for 6 bytes
            Span<byte> response = stackalloc byte[Buffer.Length + 1];
            command[0] = Command; // Set the first byte
            Buffer.CopyTo(command[1..]); // Copy the rest
            device?.TransferFullDuplex(command, response);
        }
        public void SendCommand(byte Command, byte[] Buffer)
        {
            SendCommand(Command, Buffer.AsSpan());
        }
        public void WriteRegister<T>(ref T Register) where T : struct, IRegister
        {

            if (Register.LENGTH == 1)
            {
                Span<byte> command = [Register.WRITE, (byte)Register.VALUE];
                Span<byte> response = [0, 0];
                device?.TransferFullDuplex(command, response);
            }
            else
            {
                Span<byte> command =
                [
                    Register.WRITE,
                    (byte)(Register.VALUE >> 32),
                    (byte)(Register.VALUE >> 24),
                    (byte)(Register.VALUE >> 16),
                    (byte)(Register.VALUE >> 8),
                    (byte)(Register.VALUE >> 0),
                ];
                Span<byte> response = [0,0,0,0,0,0];
                device?.TransferFullDuplex(command, response);
            }

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
                gpioController?.Dispose();
                device?.Dispose();
                ft_device?.Dispose();

                disposedValue = true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FT232HDriver()
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
        public void EditRegister<T>(RefAction<T> Editor) where T : struct, IRegister
        {
            ReadRegister(out T register);
            Editor(ref register);
            WriteRegister(ref register);
        }

        private void Irq_ValueChanged(object sender, PinValueChangedEventArgs args)
        {
            //if (DataReceived is object)
            //{
            //    DataReceived(sender, new DataReceivedEventArgs(Receive(_packetSize).ToArray()));
            //}
            ;

        }
    }
}