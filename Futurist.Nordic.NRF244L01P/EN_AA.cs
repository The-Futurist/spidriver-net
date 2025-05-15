namespace Radio.Nordic
{
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
}