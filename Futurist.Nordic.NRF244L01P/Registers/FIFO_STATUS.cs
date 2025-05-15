using static Radio.Nordic.NRF24L01P.Literals;


namespace Radio.Nordic.NRF24L01P
{
    public class FIFO_STATUS : REGISTER_SHORT
    {
        public FIFO_STATUS()
        {
            Id = 0x17;
        }
        public bool TX_REUSE => BIT6;
        public bool TX_FULL => BIT5;
        public bool TX_EMPTY => BIT4;
        public bool RX_FULL => BIT1;
        public bool RX_EMPTY => BIT0;
    }
}