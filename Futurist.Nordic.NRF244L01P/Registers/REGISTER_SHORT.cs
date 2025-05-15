namespace Radio.Nordic.NRF24L01P
{
    public abstract class REGISTER_SHORT : REGISTER
    {
        public REGISTER_SHORT()
        {
            Register = new byte[1];
            Length = 1;
        }
        public bool BIT0
        {
            get { return (Register[0] & BIT.ZERO) > 0; }
            set { Register[0] = (byte)((Register[0] & ~BIT.ZERO) | (value ? BIT.ZERO : 0x00)); }
        }
        public bool BIT1
        {
            get { return (Register[0] & BIT.ONE) > 0; }
            set { Register[0] = (byte)((Register[0] & ~BIT.ONE) | (value ? BIT.ONE : 0x00)); }
        }
        public bool BIT2
        {
            get { return (Register[0] & BIT.TWO) > 0; }
            set { Register[0] = (byte)((Register[0] & ~BIT.TWO) | (value ? BIT.TWO : 0x00)); }
        }
        public bool BIT3
        {
            get { return (Register[0] & BIT.THREE) > 0; }
            set { Register[0] = (byte)((Register[0] & ~BIT.THREE) | (value ? BIT.THREE : 0x00)); }
        }
        public bool BIT4
        {
            get { return (Register[0] & BIT.FOUR) > 0; }
            set { Register[0] = (byte)((Register[0] & ~BIT.FOUR) | (value ? BIT.FOUR : 0x00)); }
        }
        public bool BIT5
        {
            get { return (Register[0] & BIT.FIVE) > 0; }
            set { Register[0] = (byte)((Register[0] & ~BIT.FIVE) | (value ? BIT.FIVE : 0x00)); }
        }
        public bool BIT6
        {
            get { return (Register[0] & BIT.SIX) > 0; }
            set { Register[0] = (byte)((Register[0] & ~BIT.SIX) | (value ? BIT.SIX : 0x00)); }
        }
        public bool BIT7
        {
            get { return (Register[0] & BIT.SEVEN) > 0; }
            set { Register[0] = (byte)((Register[0] & ~BIT.SEVEN) | (value ? BIT.SEVEN : 0x00)); }
        }
    }
}