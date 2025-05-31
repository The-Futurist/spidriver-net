using System.Management;
using static Radio.Nordic.NRF24L01P.Pipe;
using static Radio.Nordic.NRF24L01P.DataRate;
using static Radio.Nordic.NRF24L01P.CRC;

// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{

    public class NRF24L01P : IDisposable
    {
        private bool disposedValue;
        private int channel = 0;
        private int frequency = 2400;
        private int retries = 3;
        private int interval;
        private IRadioDriver driver;

        public static NRF24L01P Create(DriverSettings Settings)
        {
            var driver = DriverFactory.CreateDriver(Settings);
            return new NRF24L01P (driver);
        }

        private NRF24L01P(IRadioDriver IODriver)
        {
            driver = IODriver;
        }
        public void Connect()
        {
            driver.Connect();
        }
        public static bool TryGetNrfComPort(out string Port)
        {
            Port = String.Empty;

            using var searcher = new ManagementObjectSearcher("SELECT Manufacturer, Name FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'").Get();

            foreach (var obj in searcher)
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
        public void ReadRegister<T>(out T register) where T : struct, IREGISTER
        {
            driver.ReadRegister(out register);
        }
        public void WriteRegister<T>(ref T register) where T : struct, IREGISTER
        {
            driver.WriteRegister(ref register);
        }
        /// <summary>
        /// The CS pin on the SPIDriver when set to true means "select" or "activate" and actually makes the pin low.
        /// </summary>
        public Pin CS
        {
            set => driver.CS = value;
        }
        public Pin CE
        {
            set => driver.CE = value;
        }
        public int Channel { get => channel; }
        public int Interval { get => interval; }
        public int Retries { get => retries; }
        public int Frequency { get => frequency; }
        public void SetCSLow()
        {
            CS = Pin.Low; //device.SetOutput(Output.CS, true);
        }
        public void SetCSHigh()
        {
            CS = Pin.High; //device.SetOutput(Output.CS, false);
        }
        public void SetCELow()
        {
            CE = Pin.Low; // device.SetOutput(ce_pin, false);
        }
        public void SetCEHigh()
        {
            CE = Pin.High; // device.SetOutput(ce_pin, true);
        }

        public void Reset()
        {
            CONFIG config = default;
            EN_AA en_aa = default;
            EN_RXADDR en_rxaddr = default;
            SETUP_AW setup_aw = default;
            SETUP_RETR setup_retr = default;
            RF_CH rf_ch = default;
            RF_SETUP rf_setup = default;
            STATUS status = default;
            RX_ADDR_P0 rx_addr_p0 = default ;
            RX_ADDR_P1 rx_addr_p1 = default;
            RX_ADDR_P2 rx_addr_p2 = default;
            RX_ADDR_P3 rx_addr_p3 = default ;
            RX_ADDR_P4 rx_addr_p4 = default;
            RX_ADDR_P5 rx_addr_p5 = default;
            TX_ADDR tx_addr = default   ;
            RX_PW_P0 rx_pw0 = default;
            RX_PW_P1 rx_pw1 = default;
            RX_PW_P2 rx_pw2 = default;
            RX_PW_P3 rx_pw3 = default;
            RX_PW_P4 rx_pw4 = default;
            RX_PW_P5 rx_pw5 = default;
            FEATURE feature = default;

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

            rx_addr_p0.ADDRESS = 0xE7E7E7E7E7;

            rx_addr_p1.ADDRESS = 0xC2C2C2C2C2;

            rx_addr_p2.VALUE = 0xC3;
            rx_addr_p3.VALUE = 0xC4;
            rx_addr_p4.VALUE = 0xC5;
            rx_addr_p5.VALUE = 0xC6;

            tx_addr.ADDRESS = 0xE7E7E7E7E7;

            feature.EN_DYN_ACK = false;
            feature.EN_DPL = false;
            feature.EN_ACK_PAY = false;

            WriteRegister(ref config);
            WriteRegister(ref en_aa);
            WriteRegister(ref en_rxaddr);
            WriteRegister(ref setup_aw);
            WriteRegister(ref setup_retr);
            WriteRegister(ref rf_ch);
            WriteRegister(ref rf_setup);
            WriteRegister(ref status);
            WriteRegister(ref rx_addr_p0);
            WriteRegister(ref rx_addr_p1);
            WriteRegister(ref rx_addr_p2);
            WriteRegister(ref rx_addr_p3);
            WriteRegister(ref rx_addr_p4);
            WriteRegister(ref rx_addr_p5);
            WriteRegister(ref tx_addr);
            WriteRegister(ref rx_pw0);
            WriteRegister(ref rx_pw1);
            WriteRegister(ref rx_pw2);
            WriteRegister(ref rx_pw3);
            WriteRegister(ref rx_pw4);
            WriteRegister(ref rx_pw5);
            WriteRegister(ref feature);

            FlushTransmitFifo();
            FlushReceiveFifo();
        }
        public void ConfigureRadio(byte Channel, OutputPower Power, DataRate Rate)
        {
            if (channel > 124)
                throw new ArgumentException("The value must be >= 0 and <= 124.", nameof(Channel));

            ReadRegister<RF_CH>(out var rf_ch);

            rf_ch.CH = Channel;

            WriteRegister(ref rf_ch);

            ReadRegister<RF_SETUP>(out var rf_setup);

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

            WriteRegister(ref rf_setup);

            channel = Channel;
            frequency = 2400 + channel;

        }
        public void ClearInterruptFlags(bool RX_DR, bool TX_DS, bool MAX_RT)
        {
            // The bits in the NRF are write 1 to clear, so setting them true, clears that interrupt

            
            ReadRegister<STATUS>(out var status);

            status.RX_DR = RX_DR;
            status.TX_DS = TX_DS;
            status.MAX_RT = MAX_RT;

            WriteRegister(ref status);
        }
        public void SetPipeState(Pipe Pipe, bool Active)
        {
            ReadRegister<EN_RXADDR>(out var reg);

            switch (Pipe)
            {
                case Pipe_0:
                    {
                        reg.ERX_P0 = Active;
                        break;
                    }
                case Pipe_1:
                    {
                        reg.ERX_P1 = Active;
                        break;
                    }
                case Pipe_2:
                    {
                        reg.ERX_P2 = Active;
                        break;
                    }
                case Pipe_3:
                    {
                        reg.ERX_P3 = Active;
                        break;
                    }
                case Pipe_4:
                    {
                        reg.ERX_P4 = Active;
                        break;
                    }
                case Pipe_5:
                    {
                        reg.ERX_P5 = Active;
                        break;
                    }
            }

            WriteRegister(ref reg);
        }
        public void SetAutoAck(Pipe Pipe, bool State)
        {
            ReadRegister<EN_AA>(out var reg);

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

            WriteRegister(ref reg);
        }

        public void SetDirection(Direction Direction )
        {
            ReadRegister<CONFIG>(out var reg);

            if (Direction == Direction.Transmit)
                reg.PRIM_RX = false;
            else
                reg.PRIM_RX = true;
            
            WriteRegister(ref reg);
        }

        public void SetCRC(bool EnableCrc, CRC CrcSize)
        {
            ReadRegister<CONFIG>(out var reg);
            reg.EN_CRC = EnableCrc;
            reg.CRCO = CrcSize == OneByte ? false : true;
            WriteRegister(ref reg);
        }

        public void SetAddressWidth(byte ByteWidth)
        {
            if (ByteWidth < 3 || ByteWidth > 5)
                throw new ArgumentException("Value must be >= 3 and <= 5", nameof(ByteWidth));

            ReadRegister<SETUP_AW>(out var reg);
            reg.AW = (byte)(ByteWidth - 2);
            WriteRegister(ref reg);
        }

        public void SetAutoAckRetries(byte Interval, byte MaxRetries)
        {
            if (Interval > 15)
                throw new ArgumentException("Value must be >= 0 and <= 15", nameof(Interval));

            if (MaxRetries > 15)
                throw new ArgumentException("Value must be >= 0 and <= 15", nameof(MaxRetries));

            ReadRegister<SETUP_RETR>(out var reg);
            reg.ARD = Interval;
            reg.ARC = MaxRetries;
            WriteRegister(ref reg);

            interval = 250 + (250 * Interval);
            retries = MaxRetries;
        }

        public void SetDynamicPayload(bool State)
        {
            ReadRegister<FEATURE>(out var ftr);
            ftr.EN_DPL = State;
            WriteRegister(ref ftr);
        }

        public void SetDynamicPayloadPipe(Pipe Pipe, bool State)
        {
            ReadRegister<DYNPD>(out var dyn);

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

            WriteRegister(ref dyn);
        }
        public void SetDynamicAck(bool State)
        {
            ReadRegister<FEATURE>(out var ftr);
            ftr.EN_DYN_ACK = State;
            WriteRegister(ref ftr);
        }
        public void PowerUp()
        {
            ReadRegister<CONFIG>(out var reg);
            reg.PWR_UP = true;
            WriteRegister(ref reg);
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
                        reg.ADDRESS = Address.ADDRESS;
                        WriteRegister(ref reg);
                        break;
                    }
                case Pipe_1:
                    {
                        RX_ADDR_P1 reg = new();
                        reg.ADDRESS = Address.ADDRESS;
                        WriteRegister(ref reg);
                        break;
                    }
            }
        }

        public void SetTransmitAddress(Address Address)
        {
            TX_ADDR txaddr = new();
            txaddr.ADDRESS = Address.ADDRESS;
            WriteRegister(ref txaddr);
        }

        public void SendPayload(byte[] Buffer, bool Ack)
        {
            SendPayload(Buffer, Buffer.Length, Ack);
        }
        public void SendPayload(byte[] Buffer, int Bytes, bool Ack)
        {
            if (Bytes > Buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(Bytes));

            SetCSLow();

            if (Ack)
                driver.SendCommand(COMMAND.W_TX_PAYLOAD, Buffer);
            else
                driver.SendCommand(COMMAND.W_TX_PAYLOAD_NO_ACK, Buffer);

            SetCSHigh();

            // We must pulse the CE pin for > 10 uS, this code strives to spin for about 20 uS

            SetCEHigh();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            while (stopwatch.ElapsedTicks < (20 * (System.Diagnostics.Stopwatch.Frequency / 1_000_000))) { }
            SetCELow();

        }
        public void FlushTransmitFifo()
        {
            driver.SendCommand(COMMAND.FLUSH_TX);
        }
        public void FlushReceiveFifo()
        {
            driver.SendCommand(COMMAND.FLUSH_RX);
        }

        public STATUS PollStatusUntil(Func<STATUS, bool> func)
        {
            ReadRegister<STATUS>(out var reg);

            while (!func(reg))
            {
                ReadRegister<STATUS>(out reg);
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
                driver.Close();
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