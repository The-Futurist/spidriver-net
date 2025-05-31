namespace Radio.Nordic.NRF24L01P
{
    public struct RX_ADDR_P4 : IRegister
    {

        private REGISTER bits;
        public byte REGID => 0x0E;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public byte ADDRESS
        {
            get => (byte)VALUE; set => VALUE = value;
        }

        public int LENGTH => 1;
    }
}