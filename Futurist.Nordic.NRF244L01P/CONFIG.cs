namespace Radio.Nordic.NRF24L01P
{
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
}