using static Radio.Nordic.Literals;


namespace Radio.Nordic
{
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
}