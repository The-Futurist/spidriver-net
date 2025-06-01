namespace Radio.Nordic.NRF24L01P
{
    public struct RF_SETUP : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x06;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }

        public bool CONT_WAVE
        {
            get => bits[7]; set => bits[7] = value;
        }
        public bool RF_DR_LOW
        {
            get => bits[5]; set => bits[5] = value;
        }
        public bool PLL_LOCK
        {
            get => bits[4]; set => bits[4] = value;
        }
        public bool RF_DR_HIGH
        {
            get => bits[3]; set => bits[3] = value;
        }
        public byte RF_PWR
        {
            get => (byte)((VALUE & 0x06) >> 1);
            set
            {
                VALUE &= 0xF9;
                VALUE |= (byte)((value & 0x03) << 1);
            }
        }

        public int LENGTH => 1;
    }
}