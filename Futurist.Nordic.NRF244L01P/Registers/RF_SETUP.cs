namespace Radio.Nordic.NRF24L01P
{
    public struct RF_SETUP : IREGISTER
    {
        private REGISTER bits;
        public byte REGID => 0x06;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }

        public bool CONT_WAVE
        {
            get => bits.BIT7; set => bits.BIT7 = value;
        }
        public bool RF_DR_LOW
        {
            get => bits.BIT5; set => bits.BIT5 = value;
        }
        public bool PLL_LOCK
        {
            get => bits.BIT4; set => bits.BIT4 = value;
        }
        public bool RF_DR_HIGH
        {
            get => bits.BIT3; set => bits.BIT3 = value;
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