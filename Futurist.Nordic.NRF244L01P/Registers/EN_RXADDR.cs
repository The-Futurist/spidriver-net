namespace Radio.Nordic.NRF24L01P
{
    public struct EN_RXADDR : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x02;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool ERX_P0
        {
            get => bits[0]; set => bits[0] = value;
        }
        public bool ERX_P1
        {
            get => bits[1]; set => bits[1] = value;
        }
        public bool ERX_P2
        {
            get => bits[2]; set => bits[2] = value;
        }
        public bool ERX_P3
        {
            get => bits[3]; set => bits[3] = value;
        }
        public bool ERX_P4
        {
            get => bits[4]; set => bits[4] = value;
        }
        public bool ERX_P5
        {
            get => bits[5]; set => bits[5] = value;
        }

        public int LENGTH => 1;
    }
}