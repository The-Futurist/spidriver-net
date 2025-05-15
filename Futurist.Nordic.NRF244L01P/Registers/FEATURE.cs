using static Radio.Nordic.NRF24L01P.Literals;


namespace Radio.Nordic.NRF24L01P
{
    public class FEATURE : REGISTER_SHORT
    {
        public FEATURE()
        {
            Id = 0x1D;
        }
        public bool EN_DYN_ACK
        {
            get => BIT0; set => BIT0 = value;
        }
        public bool EN_ACK_PAY
        {
            get => BIT1; set => BIT1 = value;
        }
        public bool EN_DPL
        {
            get => BIT2; set => BIT2 = value;
        }
    }
}