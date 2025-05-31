namespace Radio.Nordic.NRF24L01P
{
    public struct TX_ADDR : IRegister
    {

        private REGISTER_LONG bits;
        public byte REGID => 0x10;
        public ulong VALUE { get => bits.BYTES; set => bits.BYTES = value; }
        public ulong ADDRESS
        {
            get => bits.BYTES; set => bits.BYTES = value;
        }

        public int LENGTH => 5;
    }
}