namespace Radio.Nordic.NRF24L01P
{
    public struct RX_ADDR_P3 : IREGISTER
    {

        private REGISTER bits;
        public byte ADDR => 0x0D;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public byte ADDRESS
        {
            get => (byte)VALUE; set => VALUE = value;
        }

        public int LENGTH => 1;
    }
}