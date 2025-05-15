using static Radio.Nordic.NRF24L01P.Literals;


namespace Radio.Nordic.NRF24L01P
{
    public class RF_SETUP : REGISTER_SHORT
    {
        public RF_SETUP()
        {
            Id = 0x06;
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
}