using SpiDriver;
using System.Management;
using static Radio.Nordic.NRF24L01P.Pipe;
using static Radio.Nordic.NRF24L01P.DataRate;

// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public class NRF24L01P(string comport, Output CEPin) : IDisposable
    {
        private readonly Device device = new(comport);
        private bool disposedValue;
        private int channel = 0;
        private int frequency = 2400;
        private int retries = 3;
        private int interval;
        private readonly Output ce_pin = CEPin;

        public void ConnectUSB()
        {
            device.Connect();
            CS = Pin.High;
            CE = Pin.Low;
        }
        public static bool TryGetNrfComPort(out string Port)
        {
            Port = null;

            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'");
            foreach (var obj in searcher.Get())
            {
                if (obj["Manufacturer"].ToString() == "FTDI")
                {
                    var s = obj["Name"].ToString().Split(['(', ')'], 10);
                    Port = s[1];
                    return true;
                }
            }

            return false;
        }
        public T ReadRegister<T>() where T : REGISTER, new()
        {
            T register = new();
            SetCSLow();
            device.Write([COMMAND.R_REGISTER.OR(register.Id)], 0, 1);
            device.Read(register.Register, 0, register.Length);
            SetCSHigh();
            return register;
        }
        public void WriteRegister(REGISTER register)
        {
            SetCSLow();
            device.Write([COMMAND.W_REGISTER.OR(register.Id)], 0, 1);
            device.Write(register.Register, 0, register.Length);
            SetCSHigh();
        }
        public Pin CS
        {
            set => device.SetOutput(Output.CS, value == Pin.Low);
        }
        public Pin CE
        {
            set => device.SetOutput(ce_pin, value != Pin.Low);
        }
        public int Channel { get => channel; }
        public int Interval { get => interval; }
        public int Retries { get => retries; }
        public int Frequency { get => frequency; }
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
            device.SetOutput(ce_pin, false);
        }
        public void SetCEHigh()
        {
            device.SetOutput(ce_pin, true);
        }

        public void Reset()
        {
            CONFIG config = new();
            EN_AA en_aa = new();
            EN_RXADDR en_rxaddr = new();
            SETUP_AW setup_aw = new();
            SETUP_RETR setup_retr = new();
            RF_CH rf_ch = new();
            RF_SETUP rf_setup = new();
            STATUS status = new();
            RX_ADDR_P0 rx_addr_p0 = new();
            RX_ADDR_P1 rx_addr_p1 = new();
            RX_ADDR_P2 rx_addr_p2 = new();
            RX_ADDR_P3 rx_addr_p3 = new();
            RX_ADDR_P4 rx_addr_p4 = new();
            RX_ADDR_P5 rx_addr_p5 = new();
            TX_ADDR tx_addr = new();
            RX_PW_P0 rx_pw0 = new();
            RX_PW_P1 rx_pw1 = new();
            RX_PW_P2 rx_pw2 = new();
            RX_PW_P3 rx_pw3 = new();
            RX_PW_P4 rx_pw4 = new();
            RX_PW_P5 rx_pw5 = new();
            FEATURE feature= new FEATURE();

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

            interval = 250 + (setup_retr.ARD * 250);
            retries = setup_retr.ARC;

            rf_ch.CH = 2;

            channel = 2;
            frequency = 2400 + 2;

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

            feature.EN_DYN_ACK = false;
            feature.EN_DPL = false;
            feature.EN_ACK_PAY = false;

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
            WriteRegister(feature);
            FlushTransmitFifo();
            FlushReceiveFifo();
        }
        public void ConfigureRadio(byte Channel, OutputPower Power, DataRate Rate)
        {
            if (channel > 124)
                throw new ArgumentException("The value must be >= 0 and <= 124.", nameof(Channel));

            var rf_ch = ReadRegister<RF_CH>();

            rf_ch.CH = Channel;

            WriteRegister(rf_ch);

            var rf_setup = ReadRegister<RF_SETUP>();

            rf_setup.RF_PWR = (byte)Power;

            switch (Rate) // TODO validate this arg
            {
                case Min:    // min rate
                    {
                        rf_setup.RF_DR_LOW = true;
                        rf_setup.RF_DR_HIGH = false;
                        break;
                    }
                case Max:    // mmax rate
                    {
                        rf_setup.RF_DR_LOW = false;
                        rf_setup.RF_DR_HIGH = true;
                        break;
                    }
                case Med:    // med rate
                    {
                        rf_setup.RF_DR_LOW = false;
                        rf_setup.RF_DR_HIGH = false;
                        break;
                    }
            }

            WriteRegister(rf_setup);

            channel = Channel;
            frequency = 2400 + channel;

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

            switch (Pipe)
            {
                case Pipe_0:
                    {
                        reg.ERX_P0 = State;
                        break;
                    }
                case Pipe_1:
                    {
                        reg.ERX_P1 = State;
                        break;
                    }
                case Pipe_2:
                    {
                        reg.ERX_P2 = State;
                        break;
                    }
                case Pipe_3:
                    {
                        reg.ERX_P3 = State;
                        break;
                    }
                case Pipe_4:
                    {
                        reg.ERX_P4 = State;
                        break;
                    }
                case Pipe_5:
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

            switch (Pipe)
            {
                case Pipe_0:
                    {
                        reg.ENAA_P0 = State;
                        break;
                    }
                case Pipe_1:
                    {
                        reg.ENAA_P1 = State;
                        break;
                    }
                case Pipe_2:
                    {
                        reg.ENAA_P2 = State;
                        break;
                    }
                case Pipe_3:
                    {
                        reg.ENAA_P3 = State;
                        break;
                    }
                case Pipe_4:
                    {
                        reg.ENAA_P4 = State;
                        break;
                    }
                case Pipe_5:
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

        public void SetAddressWidth(byte ByteWidth)
        {
            if (ByteWidth < 3 || ByteWidth > 5)
                throw new ArgumentException("Value must be >= 3 and <= 5", nameof(ByteWidth));

            var reg = ReadRegister<SETUP_AW>();
            reg.AW = (byte)(ByteWidth - 2);
            WriteRegister(reg);
        }

        public void SetAutoAckRetries(byte Interval, byte MaxRetries)
        {
            if (Interval > 15)
                throw new ArgumentException("Value must be >= 0 and <= 15", nameof(Interval));

            if (MaxRetries > 15)
                throw new ArgumentException("Value must be >= 0 and <= 15", nameof(MaxRetries));

            var reg = ReadRegister<SETUP_RETR>();
            reg.ARD = Interval;
            reg.ARC = MaxRetries;
            WriteRegister(reg);

            interval = 250 + (250 * Interval);
            retries = MaxRetries;
        }

        public void SetDynamicPayload(bool State)
        {
            var ftr = ReadRegister<FEATURE>();
            ftr.EN_DPL = State;
            WriteRegister(ftr);
        }

        public void SetDynamicPipe(Pipe Pipe, bool State)
        {
            var dyn = ReadRegister<DYNPD>();

            switch (Pipe)
            {
                case Pipe_0:
                    dyn.DPL_P0 = State;
                    break;
                case Pipe_1:
                    dyn.DPL_P1 = State;
                    break;
                case Pipe_2:
                    dyn.DPL_P2 = State;
                    break;
                case Pipe_3:
                    dyn.DPL_P3 = State;
                    break;
                case Pipe_4:
                    dyn.DPL_P4 = State;
                    break;
                case Pipe_5:
                    dyn.DPL_P5 = State;
                    break;
            }

            WriteRegister(dyn);
        }
        public void SetDynamicAck(bool State)
        {
            var ftr = ReadRegister<FEATURE>();
            ftr.EN_DYN_ACK = State;
            WriteRegister(ftr);
        }
        public void PowerUp()
        {
            var reg = ReadRegister<CONFIG>();
            reg.PWR_UP = true;
            WriteRegister(reg);
            Thread.Sleep(2); // 1.5 mS or more is the required settling time. 
        }

        public void SetReceiveAddressLong(Address Address, Pipe Pipe)
        {
            if (Pipe != Pipe.Pipe_0 && Pipe != Pipe.Pipe_1)
                throw new ArgumentException("Only pipe's 0 and 1 are permitted.", nameof(Pipe));

            switch (Pipe)
            {
                case Pipe_0:
                    {
                        RX_ADDR_P0 reg = new();
                        reg.ADDR = Address.Bytes;
                        WriteRegister(reg);
                        break;
                    }

                case Pipe_1:
                    {
                        RX_ADDR_P1 reg = new();
                        reg.ADDR = Address.Bytes;
                        WriteRegister(reg);
                        break;
                    }
            }
        }

        public void SetTransmitAddress(Address Address)
        {
            TX_ADDR txaddr = new();
            txaddr.ADDR = Address.Bytes;
            WriteRegister(txaddr);
        }

        public void SendPayload(byte[] Buffer, bool NoAck = false)
        {
            SendPayload(Buffer, Buffer.Length, NoAck);
        }
        public void SendPayload(byte[] Buffer, int Bytes, bool NoAck = false)
        {
            if (Bytes > Buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(Bytes));

            SetCSLow();

            if (NoAck)
            {
                device.Write([COMMAND.W_TX_PAYLOAD_NO_ACK], 0, 1);
            }
            else
            {
                device.Write([COMMAND.W_TX_PAYLOAD], 0, 1);
            }

            device.Write(Buffer, 0, Bytes);
            SetCSHigh();

            // We must pulse the CE pin for > 10 uS, this code strives to spin for about 20 uS

            SetCEHigh();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            while (stopwatch.ElapsedTicks < (20 * (System.Diagnostics.Stopwatch.Frequency / 1_000_000))) { }
            SetCELow();

        }
        public void FlushTransmitFifo()
        {
            SetCSLow();
            device.Write([COMMAND.FLUSH_TX], 0, 1);
            SetCSHigh();
        }
        public void FlushReceiveFifo()
        {
            SetCSLow();
            device.Write([COMMAND.FLUSH_RX], 0, 1);
            SetCSHigh();
        }

        public STATUS PollStatusUntil(Func<STATUS, bool> func)
        {
            var reg = ReadRegister<STATUS>();

            while (!func(reg))
            {
                reg = ReadRegister<STATUS>();
            }

            return reg;
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