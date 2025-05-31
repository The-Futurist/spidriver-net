namespace Radio.Nordic.NRF24L01P
{
    public struct OBSERVE_TX : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x08;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public byte PLOS_CNT => (byte)((VALUE & 0xF0) >> 4);
        public byte ARC_CNT => (byte)(VALUE & 0x0F);

        public int LENGTH => 1;
    }
}