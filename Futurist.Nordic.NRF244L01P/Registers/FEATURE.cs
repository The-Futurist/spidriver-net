namespace Radio.Nordic.NRF24L01P
{
    public struct FEATURE : IREGISTER
    {
        private REGISTER bits;
        public byte ADDR => 0x1D;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool EN_DYN_ACK
        {
            get => bits.BIT0; set => bits.BIT0 = value;
        }
        public bool EN_ACK_PAY
        {
            get => bits.BIT1; set => bits.BIT1 = value;
        }
        public bool EN_DPL
        {
            get => bits.BIT2; set => bits.BIT2 = value;
        }

        public int LENGTH => 1;
    }
}