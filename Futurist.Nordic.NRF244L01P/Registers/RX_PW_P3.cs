namespace Radio.Nordic.NRF24L01P
{
    public struct RX_PW_P3 : IREGISTER
    {
        private REGISTER bits;
        public byte ADDR => 0x14;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public byte RX_PW
        {
            get => (byte)VALUE; set => VALUE = value;
        }

        public int LENGTH => 1;
    }
}