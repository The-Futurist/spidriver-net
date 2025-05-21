namespace Radio.Nordic.NRF24L01P
{
    public unsafe struct RF_SETUP : IREGISTER
    {
        public REGISTER_SHORT Register;

        public RF_SETUP()
        {
            Register.Id = 0x06;
        }

        public bool CONT_WAVE
        {
            get => Register.BIT7; set => Register.BIT7 = value;
        }
        public bool RF_DR_LOW
        {
            get => Register.BIT5; set => Register.BIT5 = value;
        }
        public bool PLL_LOCK
        {
            get => Register.BIT4; set => Register.BIT4 = value;
        }
        public bool RF_DR_HIGH
        {
            get => Register.BIT3; set => Register.BIT3 = value;
        }
        public byte RF_PWR
        {
            get => (byte)((Register.Register[0] & 0x06) >> 1);
            set
            {
                Register.Register[0] &= 0xF9;
                Register.Register[0] |= (byte)((value & 0x03) << 1);
            }
        }

        public byte Id => Register.Id;

        public int Length => Register.Length;

    }
}