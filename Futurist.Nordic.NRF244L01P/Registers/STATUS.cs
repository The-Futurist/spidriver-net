namespace Radio.Nordic.NRF24L01P
{
    public class STATUS : REGISTER_SHORT
    {
        public STATUS()
        {
            Id = 0x07;
        }
        public bool RX_DR => BIT6;
        public bool TX_DS => BIT5;
        public bool MAX_RT => BIT4;
        public byte RX_P_NO => (byte)((Register[0] & 0x0E) >> 1);
        public bool TX_FULL => BIT0;
    }
}