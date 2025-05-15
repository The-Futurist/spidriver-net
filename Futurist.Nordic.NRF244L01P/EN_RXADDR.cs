namespace Radio.Nordic
{
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
}