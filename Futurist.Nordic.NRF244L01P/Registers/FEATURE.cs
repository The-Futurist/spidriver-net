namespace Radio.Nordic.NRF24L01P
{
    public struct FEATURE : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x1D;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool EN_DYN_ACK
        {
            get => bits[0]; set => bits[0] = value;
        }
        public bool EN_ACK_PAY
        {
            get => bits[1]; set => bits[1] = value;
        }
        public bool EN_DPL
        {
            get => bits[2]; set => bits[2] = value;
        }

        public int LENGTH => 1;
    }
}