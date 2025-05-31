// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf
using Iot.Device.Ft232H;
using Iot.Device.FtCommon;
using System.Collections;
using System.Device.Gpio;
using System.Device.Spi;
using UnitsNet;

namespace Radio.Nordic.NRF24L01P.Drivers
{
    /// <summary>
    /// Implements the driver logic for the FTDI FT232H chip. 
    /// </summary>
    /// <remarks>
    /// This driver supports the Adafruit FT232H Breakout board. 
    /// </remarks>
    /// <param name="comport"></param>
    /// <param name="CEPin"></param>
    public class FT232HDriver: IRadioDriver
    {
        private SpiDevice device;
        private GpioController gpioController;
        private int ce_pin;
        public Pin CS { set { } }  // the IoT SPI stuff uses CSN implicitly, automatically.
        public Pin CE 
        {
            set => gpioController.Write(ce_pin, value == Pin.High ? PinValue.High : PinValue.Low);
        }

        public FT232HDriver()
        {
            var devices = FtCommon.GetDevices();
            var ft232h = new Ft232HDevice(devices[0]);
            var settings = new SpiConnectionSettings(0, 3) { ClockFrequency = 10_000_000, DataBitLength = 8, ChipSelectLineActiveState = PinValue.Low };
            gpioController = ft232h.CreateGpioController();

            ce_pin = Ft232HDevice.GetPinNumberFromString("D4");
            gpioController.OpenPin(ce_pin, PinMode.Output);
            device = ft232h.CreateSpiDevice(settings);
        }

        public void Close()
        {
            //throw new NotImplementedException();
        }

        public void Connect()
        {
        }

        public void ReadRegister<T>(out T Register) where T : struct, IREGISTER
        {
            Register = default;

            if (Register.LENGTH == 1)
            {
                Span<byte> command = [Register.READ, 0xFF];
                Span<byte> response = [0, 0];
                device.TransferFullDuplex(command, response);
                Register.VALUE = response[1];
            }
            else
            {
                Span<byte> command = [Register.READ, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF];  // 6 bytes
                Span<byte> response = [0, 0, 0, 0, 0, 0];
                device.TransferFullDuplex(command, response);
                ulong value = 0;
                value |= (ulong)response[5];
                value |= (ulong)response[4] << 8;
                value |= (ulong)response[3] << 16;
                value |= (ulong)response[2] << 24;
                value |= (ulong)response[1] << 32;
                Register.VALUE = value;
            }
        }

        public void SendCommand(byte Command)
        {
            Span<byte> command = [Command];
            Span<byte> response = [0];
            device.TransferFullDuplex(command, response);
        }

        public void SendCommand(byte Command, byte[] Buffer)
        {
            Span<byte> command = stackalloc byte[Buffer.Length + 1]; // Allocate space for 6 bytes
            Span<byte> response = stackalloc byte[Buffer.Length + 1];
            command[0] = Command; // Set the first byte
            Buffer.CopyTo(command.Slice(1)); // Copy the rest
            device.TransferFullDuplex(command, response);
        }

        public void WriteRegister<T>(ref T Register) where T : struct, IREGISTER
        {

            if (Register.LENGTH == 1)
            {
                Span<byte> command = [Register.WRITE, (byte)Register.VALUE];
                Span<byte> response = [0, 0];
                device.TransferFullDuplex(command, response);
            }
            else
            {
                Span<byte> command = stackalloc byte[6];
                command[0] = Register.WRITE;
                // Extract the least significant 5 bytes of the ulong
                Span<byte> ulongBytes = BitConverter.GetBytes(Register.VALUE);
                ulongBytes.Slice(0, 5).CopyTo(command.Slice(1)); // Copy 5 bytes after the first byte
                Span<byte> response = [0,0,0,0,0,0];
                device.TransferFullDuplex(command, response);
            }

        }
    }
}