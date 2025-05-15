namespace Radio.Nordic.NRF24L01P
{
    public class EN_RXADDR : REGISTER_SHORT
    {
        public EN_RXADDR()
        {
            Id = 0x02;
        }
        public bool ERX_P0
        {
            get => BIT0; set => BIT0 = value;
        }
        public bool ERX_P1
        {
            get => BIT1; set => BIT1 = value;
        }
        public bool ERX_P2
        {
            get => BIT2; set => BIT2 = value;
        }
        public bool ERX_P3
        {
            get => BIT3; set => BIT3 = value;
        }
        public bool ERX_P4
        {
            get => BIT4; set => BIT4 = value;
        }
        public bool ERX_P5
        {
            get => BIT5; set => BIT5 = value;
        }
    }
}