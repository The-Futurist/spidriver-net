using static Radio.Nordic.Literals;


namespace Radio.Nordic
{
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
}