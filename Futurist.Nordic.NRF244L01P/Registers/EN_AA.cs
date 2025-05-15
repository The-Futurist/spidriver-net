namespace Radio.Nordic.NRF24L01P
{
    public class EN_AA : REGISTER_SHORT
    {
        public EN_AA()
        {
            Id = 0x01;
        }
        public bool ENAA_P0
        {
            get { return BIT0; }
            set { BIT0 = value; }
        }
        public bool ENAA_P1
        {
            get { return BIT1; }
            set { BIT1 = value; }
        }
        public bool ENAA_P2
        {
            get { return BIT2; }
            set { BIT2 = value; }
        }
        public bool ENAA_P3
        {
            get { return BIT3; }
            set { BIT3 = value; }
        }
        public bool ENAA_P4
        {
            get { return BIT4; }
            set { BIT4 = value; }
        }
        public bool ENAA_P5
        {
            get { return BIT5; }
            set { BIT5 = value; }
        }
    }
}