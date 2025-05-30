namespace Radio.Nordic.NRF24L01P
{
    public struct RF_CH : IREGISTER
    {
        private REGISTER bits;
        public byte REGID => 0x05;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public byte CH
        {
            get => (byte)VALUE; set => VALUE = value;
        }

        public int LENGTH => 1;
    }
}