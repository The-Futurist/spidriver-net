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
        public void WriteRegister(REGISTER register)
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

        public void Reset()
        {
            CONFIG config = new CONFIG();
            EN_AA en_aa = new EN_AA();
            EN_RXADDR en_rxaddr = new EN_RXADDR();
            SETUP_AW setup_aw = new SETUP_AW();
            SETUP_RETR setup_retr  = new SETUP_RETR();
            RF_CH rf_ch = new RF_CH();
            RF_SETUP rf_setup = new RF_SETUP();
            STATUS status = new STATUS();
            RX_ADDR_P0 rx_addr_p0 = new RX_ADDR_P0();
            RX_ADDR_P1 rx_addr_p1 = new RX_ADDR_P1();
            RX_ADDR_P2 rx_addr_p2 = new RX_ADDR_P2();
            RX_ADDR_P3 rx_addr_p3 = new RX_ADDR_P3();
            RX_ADDR_P4 rx_addr_p4 = new RX_ADDR_P4();
            RX_ADDR_P5 rx_addr_p5 = new RX_ADDR_P5();
            TX_ADDR tx_addr = new TX_ADDR();
            RX_PW_P0 rx_pw0 = new RX_PW_P0();
            RX_PW_P1 rx_pw1 = new RX_PW_P1();
            RX_PW_P2 rx_pw2 = new RX_PW_P2();
            RX_PW_P3 rx_pw3 = new RX_PW_P3();
            RX_PW_P4 rx_pw4 = new RX_PW_P4();
            RX_PW_P5 rx_pw5 = new RX_PW_P5();

            config.EN_CRC = true;

            en_aa.ENAA_P5 = true;
            en_aa.ENAA_P4 = true;
            en_aa.ENAA_P3 = true;
            en_aa.ENAA_P2 = true;
            en_aa.ENAA_P1 = true;
            en_aa.ENAA_P0 = true;

            en_rxaddr.ERX_P0 = true;
            en_rxaddr.ERX_P1 = true;

            setup_aw.AW = 3;

            setup_retr.ARD = 0x00;
            setup_retr.ARC = 0x03;

            rf_ch.CH = 2;

            rf_setup.RF_DR_HIGH = true;
            rf_setup.RF_PWR = 3;

            status.RX_DR = false;
            status.TX_DS = false;
            status.MAX_RT = false;

            rx_addr_p0.ADDR[0] = 0xE7;
            rx_addr_p0.ADDR[1] = 0xE7;
            rx_addr_p0.ADDR[2] = 0xE7;
            rx_addr_p0.ADDR[3] = 0xE7;
            rx_addr_p0.ADDR[4] = 0xE7;

            rx_addr_p1.ADDR[0] = 0xC2;
            rx_addr_p1.ADDR[1] = 0xC2;
            rx_addr_p1.ADDR[2] = 0xC2;
            rx_addr_p1.ADDR[3] = 0xC2;
            rx_addr_p1.ADDR[4] = 0xC2;

            rx_addr_p2.ADDR = 0xC3;
            rx_addr_p3.ADDR = 0xC4;
            rx_addr_p4.ADDR = 0xC5;
            rx_addr_p5.ADDR = 0xC6;

            tx_addr.ADDR[0] = 0xE7;
            tx_addr.ADDR[1] = 0xE7;
            tx_addr.ADDR[2] = 0xE7;
            tx_addr.ADDR[3] = 0xE7;
            tx_addr.ADDR[4] = 0xE7;

            WriteRegister(config);
            WriteRegister(en_aa);
            WriteRegister(en_rxaddr);
            WriteRegister(setup_aw);
            WriteRegister(setup_retr);
            WriteRegister(rf_ch);
            WriteRegister(rf_setup);
            WriteRegister(status);
            WriteRegister(rx_addr_p0);
            WriteRegister(rx_addr_p1);
            WriteRegister(rx_addr_p2);
            WriteRegister(rx_addr_p3);
            WriteRegister(rx_addr_p4);
            WriteRegister(rx_addr_p5);
            WriteRegister(tx_addr);
            WriteRegister(rx_pw0);
            WriteRegister(rx_pw1);
            WriteRegister(rx_pw2);
            WriteRegister(rx_pw3);
            WriteRegister(rx_pw4);
            WriteRegister(rx_pw5);
        }
        public void ConfigureRadio(byte Channel, byte Power, byte Rate)
        {
            var rf_ch = ReadRegister<RF_CH>();

            rf_ch.CH = Channel;

            WriteRegister(rf_ch);

            var rf_setup = ReadRegister<RF_SETUP>();

            rf_setup.RF_PWR = Power;

            switch (Rate) // TODO validate this arg
            {
                case 1:    // min rate
                    {
                        rf_setup.RF_DR_LOW = true;
                        rf_setup.RF_DR_HIGH = false;
                        break;
                    }
                case 2:    // mmax rate
                    {
                        rf_setup.RF_DR_LOW = false;
                        rf_setup.RF_DR_HIGH = true;
                        break;
                    }
                case 0:    // med rate
                    {
                        rf_setup.RF_DR_LOW = false;
                        rf_setup.RF_DR_HIGH = false;
                        break;
                    }
            }

            WriteRegister(rf_setup);
        }
        public void ClearInterruptFlags(bool RX_DR, bool TX_DS, bool MAX_RT)
        {
            // The bits in the NRF are write 1 to clear, so setting them true, clears that interrupt

            var status = ReadRegister<STATUS>();

            status.RX_DR = RX_DR;
            status.TX_DS = TX_DS;
            status.MAX_RT = MAX_RT;

            WriteRegister(status);
        }
        public void SetPipeState(Pipe Pipe, bool State)
        {
            var reg = ReadRegister<EN_RXADDR>();

            switch ((byte)Pipe)
            {
                case 0:
                    {
                        reg.ERX_P0 = State;
                        break;
                    }
                case 1:
                    {
                        reg.ERX_P1 = State;
                        break;
                    }
                case 2:
                    {
                        reg.ERX_P2 = State;
                        break;
                    }
                case 3:
                    {
                        reg.ERX_P3 = State;
                        break;
                    }
                case 4:
                    {
                        reg.ERX_P4 = State;
                        break;
                    }
                case 5:
                    {
                        reg.ERX_P5 = State;
                        break;
                    }
            }

            WriteRegister(reg);
        }
        public void SetAutoAck(Pipe Pipe, bool State)
        {
            var reg = ReadRegister<EN_AA>();

            switch ((byte)Pipe)
            {
                case 0:
                    {
                        reg.ENAA_P0 = State;
                        break;
                    }
                case 1:
                    {
                        reg.ENAA_P1 = State;
                        break;
                    }
                case 2:
                    {
                        reg.ENAA_P2 = State;
                        break;
                    }
                case 3:
                    {
                        reg.ENAA_P3 = State;
                        break;
                    }
                case 4:
                    {
                        reg.ENAA_P4 = State;
                        break;
                    }
                case 5:
                    {
                        reg.ENAA_P5 = State;
                        break;
                    }
            }

            WriteRegister(reg);
        }

        public void SetTransmitMode()
        {
            var reg = ReadRegister<CONFIG>();
            reg.PRIM_RX = false;
            WriteRegister(reg);
        }

        public void SetCRC(bool EnableCrc, bool CrcSize)
        {
            var reg = ReadRegister<CONFIG>();
            reg.EN_CRC = EnableCrc;
            reg.CRCO = CrcSize;
            WriteRegister(reg);
        }

        public void SetAddressWidth(byte Width)
        {
            var reg = ReadRegister<SETUP_AW>();
            reg.AW = Width;
            WriteRegister(reg);
        }

        public void SetAutoAckRetries(byte Interval, byte MaxRetries)
        {
            var reg = ReadRegister<SETUP_RETR>();
            reg.ARD = Interval;
            reg.ARC = MaxRetries;
            WriteRegister(reg);
        }

        public void PowerUp()
        {
            var reg = ReadRegister<CONFIG>();
            reg.PWR_UP = true;
            WriteRegister(reg);
            Thread.Sleep(2); // 1.5 mS or more is the required settling time. 
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