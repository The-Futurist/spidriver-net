using SpiDriver;
using System.Management;

// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P.Drivers
{

    /// <summary>
    /// Implements the driver logic for the FTDI FT230XQ chip. 
    /// </summary>
    /// <remarks>
    /// This driver supports the Excamera Labs board 'SPIDriver'.
    /// </remarks>
    /// <param name="ComPort"></param>
    /// <param name="CEPin"></param>
    public class FT230XQDriver(string ComPort, Output CEPin) : IRadioDriver
    {
        private readonly Device device = new(ComPort);
        private readonly Output ce_pin = CEPin;
        public Pin CS { set => device.SetOutput(Output.CS, value == Pin.Low); }
        public Pin CE { set => device.SetOutput(ce_pin, value == Pin.High); }
        public static bool TryGetNrfComPort(out string Port)
        {
            Port = String.Empty;

            using var searcher = new ManagementObjectSearcher("SELECT Manufacturer, Name FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'").Get();

            foreach (var obj in searcher)
            {
                if (obj != null)
                    if (obj["Manufacturer"]?.ToString() == "FTDI")
                    {
                        var s = obj["Name"]?.ToString()?.Split(['(', ')'], 10);
                        if (s != null)
                        {
                            Port = s[1];
                            return true;
                        }
                    }
            }

            return false;
        }
        public void Close()
        {
            device.Close();
        }
        public void Connect()
        {
            device.Connect();
            CS = Pin.High;
            CE = Pin.Low;
        }

        public void EditRegister<T>(RefAction<T> Editor) where T : struct, IRegister
        {
            throw new NotImplementedException();
        }

        public void ReadRegister<T>(out T register) where T : struct, IRegister
        {
            register = default;
            byte[] reg = new byte[register.LENGTH];
            CS = Pin.Low;
            device.Write([COMMAND.R_REGISTER.OR(register.REGID)], 0, 1);
            device.Read(reg, 0, register.LENGTH);
            CS = Pin.High;

            if (register.LENGTH == 1)
            {
                register.VALUE = reg[0];
            }
            else
            {
                ulong value = 0;
                value |= (ulong)reg[4];
                value |= (ulong)reg[3] << 8;
                value |= (ulong)reg[2] << 16;
                value |= (ulong)reg[1] << 24;
                value |= (ulong)reg[0] << 32;
                register.VALUE = value;
            }
        }
        public void SendCommand(byte Command)
        {
            CS = Pin.Low;
            device.Write([Command], 0, 1);
            CS = Pin.High;
        }
        public void SendCommand(byte Command, Span<byte> Buffer)
        {
            SendCommand(Command, Buffer.ToArray());
        }
        public void SendCommand(byte Command, byte[] Buffer)
        {
            byte[] buffer = new byte[Buffer.Length+1];
            buffer[0] = Command;
            Array.Copy(Buffer, 0,buffer, 1, Buffer.Length);
            CS = Pin.Low;
            device.Write(buffer, 0, buffer.Length);
            CS = Pin.High;
        }
        public void WriteRegister<T>(ref T register) where T : struct, IRegister
        {
            byte[] reg = new byte[register.LENGTH];

            CS = Pin.Low;
            device.Write([COMMAND.W_REGISTER.OR(register.REGID)], 0, 1);

            if (register.LENGTH == 1)
            {
                reg[0] = (byte)register.VALUE;
            }
            else
            {
                reg[4] = (byte)(register.VALUE >> 0);
                reg[3] = (byte)(register.VALUE >> 8);
                reg[2] = (byte)(register.VALUE >> 16);
                reg[1] = (byte)(register.VALUE >> 24);
                reg[0] = (byte)(register.VALUE >> 32);
            }

            device.Write(reg, 0, register.LENGTH);
            CS = Pin.High;
        }
    }
}