namespace Radio.Nordic.NRF24L01P
{
    public struct RPD : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x09;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool CD_0 => bits[0];

        public int LENGTH => 1;
    }
}