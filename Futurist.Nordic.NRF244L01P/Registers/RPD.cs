namespace Radio.Nordic.NRF24L01P
{
    public struct RPD : IREGISTER
    {
        private REGISTER bits;
        public byte ADDR => 0x09;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool CD_0 => bits.BIT0;

        public int LENGTH => 1;
    }
}