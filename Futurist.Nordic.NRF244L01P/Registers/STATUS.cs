namespace Radio.Nordic.NRF24L01P
{
    public class STATUS : REGISTER_SHORT
    {
        public STATUS()
        {
            Id = 0x07;
        }
        public bool RX_DR
        {
            get => BIT6;
            set => BIT6 = value;
        }
        public bool TX_DS
        {
            get => BIT5;
            set => BIT5 = value;
        }
        public bool MAX_RT
        {
            get => BIT4;
            set => BIT4 = value;
        }
        public byte RX_P_NO => (byte)((Register[0] & 0x0E) >> 1);
        public bool TX_FULL => BIT0;
    }
}