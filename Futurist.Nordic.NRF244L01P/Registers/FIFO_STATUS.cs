using static Radio.Nordic.NRF24L01P.Literals;


namespace Radio.Nordic.NRF24L01P
{
    public class FIFO_STATUS : REGISTER_SHORT
    {
        public FIFO_STATUS()
        {
            Id = 0x17;
        }
        public bool TX_REUSE
        {
            get
            {
                return (Register[0] & BIT(6)) != 0;
            }
        }
        public bool TX_FULL
        {
            get
            {
                return (Register[0] & BIT(5)) != 0;
            }
        }
        public bool TX_EMPTY
        {
            get
            {
                return (Register[0] & BIT(4)) != 0;
            }
        }
        public bool RX_FULL
        {
            get
            {
                return (Register[0] & BIT(1)) != 0;
            }
        }
        public bool RX_EMPTY
        {
            get
            {
                return (Register[0] & BIT(0)) != 0;
            }
        }
    }
}