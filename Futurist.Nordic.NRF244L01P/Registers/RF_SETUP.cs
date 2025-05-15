using static Radio.Nordic.NRF24L01P.Literals;


namespace Radio.Nordic.NRF24L01P
{
    public class RF_SETUP : REGISTER_SHORT
    {
        public RF_SETUP()
        {
            Id = 0x06;
        }

        public bool CONT_WAVE
        {
            get => BIT7; set => BIT7 = value;
        }
        public bool RF_DR_LOW
        {
            get => BIT5; set => BIT5 = value;
        }
        public bool PLL_LOCK
        {
            get => BIT4; set => BIT4 = value;
        }
        public bool RF_DR_HIGH
        {
            get => BIT3; set => BIT3 = value;
        }
        public byte RF_PWR
        {
            get => (byte)((Register[0] & 0x06) >> 1);
            set
            {
                Register[0] &= 0xF9;
                Register[0] |= (byte)((value & 0x03) << 1);
            }
        }
    }
}