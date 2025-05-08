using SpiDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using static Radio.Nordic.Literals;

namespace Radio.Nordic
{
    public enum Pin
    {
        Low,
        High
    }
    public class NRF24L01P  : IDisposable
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
            set
            {
                device.SetOutput(Output.CS, value == Pin.Low? true:false);
            }
        }

        public Pin CE
        {
            set
            {
                device.SetOutput(Output.CS, value == Pin.Low ? false : true);
            }
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

    public enum COMMNAND : byte
    {
        R_REGISTER = 0b0000_0000,
        W_REGISTER = 0b0010_0000,
        R_RX_PAYLOAD = 0b0110_0001,
        W_TX_PAYLOAD = 0b1010_0000,
        FLUSH_TX = 0b1110_0001,
        FLUSH_RX = 0b1110_0010,
        REUSE_TX_PL = 0b1110_0011,
        R_RX_PL_WID = 0b0110_0000,
        W_ACK_PAYLOAD = 0b1010_1000,
        W_TX_PAYLOAD_NO_ACK = 0b1011_0000,
        NOP = 0b11111111,
    }

    public abstract class REGISTER
    {
        private byte[] register;
        private byte id;
        private int length;

        public byte[] Register { get => register; protected set => register = value; }
        public byte Id { get => id; protected set => id = value; }
        public int Length { get => length; protected set => length = value; }
    }
    public abstract class REGISTER_SHORT : REGISTER
    {
        public REGISTER_SHORT()
        {
            Register = new byte[1];
            Length = 1;
        }
    }
    public abstract class REGISTER_LONG : REGISTER
    {
        public REGISTER_LONG()
        {
            Register = new byte[5];
            Length = 5;
        }
    }
    public class CONFIG : REGISTER_SHORT
    {

        public CONFIG()
        {
            Id = 0;
        }
        public bool RESERVED
        {
            get
            {
                return (Register[0] & 0x80) != 0;
            }
        }

        public bool MASK_RX_DR
        {
            get
            {
                return (Register[0] & 0x40) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x40;
                }
                else
                {
                    Register[0] &= 0xBF;
                }
            }
        }

        public bool MASK_TX_DS
        {
            get
            {
                return (Register[0] & 0x20) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x20;
                }
                else
                {
                    Register[0] &= 0xDF;
                }
            }
        }

        public bool MASK_MAX_RT
        {
            get
            {
                return (Register[0] & 0x10) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x10;
                }
                else
                {
                    Register[0] &= 0xEF;
                }
            }
        }

        public bool EN_CRC
        {
            get
            {
                return (Register[0] & 0x08) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x08;
                }
                else
                {
                    Register[0] &= 0xF7;
                }
            }
        }

        public bool CRCO
        {
            get
            {
                return (Register[0] & 0x04) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x04;
                }
                else
                {
                    Register[0] &= 0xFB;
                }
            }
        }

        public bool PWR_UP
        {
            get
            {
                return (Register[0] & 0x02) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x02;
                }
                else
                {
                    Register[0] &= 0xFD;
                }
            }
        }

        public bool PRIM_RX
        {
            get
            {
                return (Register[0] & 0x01) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x01;
                }
                else
                {
                    Register[0] &= 0xFE;
                }
            }
        }

        public override string ToString()
        {
            return $"MASK_RX_DR={MASK_RX_DR} MASK_TX_DS={MASK_TX_DS} MASK_MAX_RT={MASK_MAX_RT} MASK_EN_CRC={EN_CRC} CRCO={CRCO} PWR_UP={PWR_UP} PRIM_RX={PRIM_RX}";
        }
    }
    public class EN_AA : REGISTER_SHORT
    {
        public EN_AA()
        {
            Id = 1;
        }
        public bool ENAA_P0
        {
            get
            {
                return (Register[0] & 0x01) != 0;
            }

            set
            {
                if (value)
                {
                    Register[0] |= 0x01;
                }
                else
                {
                    Register[0] &= 0xFE;
                }
            }

        }
        public bool ENAA_P1
        {
            get
            {
                return (Register[0] & 0x02) != 0;
            }

            set
            {
                if (value)
                {
                    Register[0] |= 0x02;
                }
                else
                {
                    Register[0] &= 0xFD;
                }
            }
        }
        public bool ENAA_P2
        {
            get
            {
                return (Register[0] & 0x04) != 0;
            }

            set
            {
                if (value)
                {
                    Register[0] |= 0x04;
                }
                else
                {
                    Register[0] &= 0xFB;
                }
            }
        }
        public bool ENAA_P3
        {
            get
            {
                return (Register[0] & 0x08) != 0;
            }

            set
            {
                if (value)
                {
                    Register[0] |= 0x08;
                }
                else
                {
                    Register[0] &= 0xF7;
                }
            }
        }
        public bool ENAA_P4
        {
            get
            {
                return (Register[0] & 0x10) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x10;
                }
                else
                {
                    Register[0] &= 0xEF;
                }
            }
        }
        public bool ENAA_P5
        {
            get
            {
                return (Register[0] & 0x20) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x20;
                }
                else
                {
                    Register[0] &= 0xDF;
                }
            }
        }

    }
    public class EN_RXADDR : REGISTER_SHORT
    {
        public EN_RXADDR()
        {
            Id = 2;
        }
        public bool ERX_P0
        {
            get
            {
                return (Register[0] & 0x01) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x01;
                }
                else
                {
                    Register[0] &= 0xFE;
                }
            }
        }

        public bool ERX_P1
        {
            get
            {
                return (Register[0] & 0x02) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x02;
                }
                else
                {
                    Register[0] &= 0xFD;
                }
            }
        }

        public bool ERX_P2
        {
            get
            {
                return (Register[0] & 0x04) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x04;
                }
                else
                {
                    Register[0] &= 0xFB;
                }
            }
        }

        public bool ERX_P3
        {
            get
            {
                return (Register[0] & 0x08) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x08;
                }
                else
                {
                    Register[0] &= 0xF7;
                }
            }
        }

        public bool ERX_P4
        {
            get
            {
                return (Register[0] & 0x10) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x10;
                }
                else
                {
                    Register[0] &= 0xEF;
                }
            }
        }

        public bool ERX_P5
        {
            get
            {
                return (Register[0] & 0x20) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= 0x20;
                }
                else
                {
                    Register[0] &= 0xDF;
                }
            }
        }
    }
    public class SETUP_AW : REGISTER_SHORT
    {
        public SETUP_AW()
        {
            Id = 3;
        }
        public byte AW
        {
            get
            {
                return (byte)(Register[0] & 0x03);
            }
            set
            {
                Register[0] &= 0xFC;
                Register[0] |= (byte)(value & 0x03);
            }
        }
    }
    public class SETUP_RETR : REGISTER_SHORT
    {
        public SETUP_RETR()
        {
            Id = 4;
        }
        public byte ARD
        {
            get
            {
                return (byte)((Register[0] & 0xF0) >> 4);
            }
            set
            {
                Register[0] &= 0x0F;
                Register[0] |= (byte)((value & 0x0F) << 4);
            }
        }
        public byte ARC
        {
            get
            {
                return (byte)(Register[0] & 0x0F);
            }
            set
            {
                Register[0] &= 0xF0;
                Register[0] |= (byte)(value & 0x0F);
            }
        }
    }
    public class RF_CH : REGISTER_SHORT
    {
        public RF_CH()
        {
            Id = 5;
        }
        public byte CH
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RF_SETUP : REGISTER_SHORT
    {
        public RF_SETUP()
        {
            Id = 6;
        }

        public bool CONT_WAVE
        {
            get
            {
                return (Register[0] & BIT(7)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(7);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(7));
                }
            }

        }
        public bool RF_DR_LOW
        {
            get
            {
                return (Register[0] & BIT(5)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(5);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(5));
                }
            }
        }
        public bool PLL_LOCK
        {
            get
            {
                return (Register[0] & BIT(4)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(4);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(4));
                }
            }
        }
        public bool RF_DR_HIGH
        {
            get
            {
                return (Register[0] & BIT(3)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(3);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(3));
                }
            }
        }
        public byte RF_PWR
        {
            get
            {
                return (byte)((Register[0] & 0x06) >> 1);
            }
            set
            {
                Register[0] &= 0xF9;
                Register[0] |= (byte)((value & 0x03) << 1);
            }
        }
    }
    public class STATUS : REGISTER_SHORT
    {
        public STATUS()
        {
            Id = 7;
        }
        public bool RX_DR
        {
            get
            {
                return (Register[0] & 0x40) != 0;
            }
        }
        public bool TX_DS
        {
            get
            {
                return (Register[0] & 0x20) != 0;
            }
        }
        public bool MAX_RT
        {
            get
            {
                return (Register[0] & 0x10) != 0;
            }
        }
        public byte RX_P_NO
        {
            get
            {
                return (byte)((Register[0] & 0x0E) >> 1);
            }
        }
    }
    public class OBSERVE_TX : REGISTER_SHORT
    {
        public OBSERVE_TX()
        {
            Id = 9;
        }
        public byte PLOS_CNT
        {
            get
            {
                return (byte)((Register[0] & 0xF0) >> 4);
            }
        }
        public byte ARC_CNT
        {
            get
            {
                return (byte)(Register[0] & 0x0F);
            }
        }
    }
    public class RPD : REGISTER_SHORT
    {
        public RPD()
        {
            Id = 0x09;
        }
        public bool CD_0
        {
            get
            {
                return (Register[0] & 0x01) != 0;
            }
        }
    }
    public class RX_ADDR_P0 : REGISTER_LONG
    {

        public RX_ADDR_P0()
        {
            Id = 0x0A;
        }
        public byte[] ADDR
        {
            get
            {
                return Register;
            }
            set
            {
                Register = value;
            }
        }
    }
    public class RX_ADDR_P1 : REGISTER_LONG
    {

        public RX_ADDR_P1()
        {
            Id = 0x0B;
        }
        public byte[] ADDR
        {
            get
            {
                return Register;
            }
            set
            {
                Register = value;
            }
        }
    }
    public class RX_ADDR_P2 : REGISTER_SHORT
    {

        public RX_ADDR_P2()
        {
            Id = 0x0C;
        }
        public byte ADDR
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RX_ADDR_P3 : REGISTER_SHORT
    {

        public RX_ADDR_P3()
        {
            Id = 0x0D;
        }
        public byte ADDR
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RX_ADDR_P4 : REGISTER_SHORT
    {

        public RX_ADDR_P4()
        {
            Id = 0x0E;
        }
        public byte ADDR
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RX_ADDR_P5 : REGISTER_SHORT
    {

        public RX_ADDR_P5()
        {
            Id = 0x0F;
        }
        public byte ADDR
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class TX_ADDR : REGISTER_LONG
    {
        public TX_ADDR()
        {
            Id = 0x10;
        }
        public byte[] ADDR
        {
            get
            {
                return Register;
            }
            set
            {
                Register = value;
            }
        }
    }
    public class RX_PW_P0 : REGISTER_SHORT
    {
        public RX_PW_P0()
        {
            Id = 0x11;
        }
        public byte RX_PW
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RX_PW_P1 : REGISTER_SHORT
    {
        public RX_PW_P1()
        {
            Id = 0x12;
        }
        public byte RX_PW
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RX_PW_P2 : REGISTER_SHORT
    {
        public RX_PW_P2()
        {
            Id = 0x13;
        }
        public byte RX_PW
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RX_PW_P3 : REGISTER_SHORT
    {
        public RX_PW_P3()
        {
            Id = 0x14;
        }
        public byte RX_PW
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RX_PW_P4 : REGISTER_SHORT
    {
        public RX_PW_P4()
        {
            Id = 0x15;
        }
        public byte RX_PW
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class RX_PW_P5 : REGISTER_SHORT
    {
        public RX_PW_P5()
        {
            Id = 0x16;
        }
        public byte RX_PW
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
    public class FIFO_STATUS : REGISTER_SHORT
    {
        public FIFO_STATUS()
        {
            Id = 0x17;
        }
        public bool TX_REUSE
        {
            get
            {
                return (Register[0] & BIT(6)) != 0;
            }
        }
        public bool TX_FULL
        {
            get
            {
                return (Register[0] & BIT(5)) != 0;
            }
        }
        public bool TX_EMPTY
        {
            get
            {
                return (Register[0] & BIT(4)) != 0;
            }
        }
        public bool RX_FULL
        {
            get
            {
                return (Register[0] & BIT(1)) != 0;
            }
        }
        public bool RX_EMPTY
        {
            get
            {
                return (Register[0] & BIT(0)) != 0;
            }
        }
    }
    public class DYNPD : REGISTER_SHORT
    {
        public DYNPD()
        {
            Id = 0x1C;
        }
        public bool DPL_P0
        {
            get
            {
                return (Register[0] & BIT(0)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(0);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(0));
                }
            }
        }
        public bool DPL_P1
        {
            get
            {
                return (Register[0] & BIT(1)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(1);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(1));
                }
            }
        }
        public bool DPL_P2
        {
            get
            {
                return (Register[0] & BIT(2)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(2);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(2));
                }
            }
        }
        public bool DPL_P3
        {
            get
            {
                return (Register[0] & BIT(3)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(3);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(3));
                }
            }
        }
        public bool DPL_P4
        {
            get
            {
                return (Register[0] & BIT(4)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(4);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(4));
                }
            }
        }
        public bool DPL_P5
        {
            get
            {
                return (Register[0] & BIT(5)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(5);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(5));
                }
            }
        }
    }
    public class FEATURE : REGISTER_SHORT
    {
        public FEATURE()
        {
            Id = 0x1D;
        }
        public bool EN_DYN_ACK
        {
            get
            {
                return (Register[0] & BIT(0)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(0);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(0));
                }
            }
        }
        public bool EN_ACK_PAY
        {
            get
            {
                return (Register[0] & BIT(1)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(1);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(1));
                }
            }
        }
        public bool EN_DPL
        {
            get
            {
                return (Register[0] & BIT(2)) != 0;
            }
            set
            {
                if (value)
                {
                    Register[0] |= BIT(2);
                }
                else
                {
                    Register[0] &= NBYTE(BIT(2));
                }
            }
        }
    }
    public static class Literals
    {
        public static byte BYTE(int arg)
        {
            return (byte)(arg & 0xff);
        }

        public static byte NBYTE(int arg)
        {
            return (byte)(~arg & 0xff);
        }

        public static byte BIT(byte p)
        {
            return (byte)(1 << p);
        }

    }
}