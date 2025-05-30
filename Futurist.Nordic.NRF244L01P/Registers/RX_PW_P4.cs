namespace Radio.Nordic.NRF24L01P
{
    public struct RX_PW_P4 : IREGISTER
    {
        private REGISTER bits;
        public byte REGID => 0x15;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public byte RX_PW
        {
            get => (byte)VALUE; set => VALUE = value;
        }

        public int LENGTH => 1;
    }
}