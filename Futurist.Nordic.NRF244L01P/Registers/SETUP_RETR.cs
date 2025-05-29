namespace Radio.Nordic.NRF24L01P
{
    public struct SETUP_RETR : IREGISTER
    {
        private REGISTER bits;
        public byte ADDR => 0x04;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public byte ARD
        {
            get => (byte)((VALUE & 0xF0) >> 4);
            set
            {
                VALUE &= 0x0F;
                VALUE |= (byte)((value & 0x0F) << 4);
            }
        }
        public byte ARC
        {
            get => (byte)(VALUE & 0x0F);
            set
            {
                VALUE &= 0xF0;
                VALUE |= (byte)(value & 0x0F);
            }
        }

        public int LENGTH => 1;
    }
}