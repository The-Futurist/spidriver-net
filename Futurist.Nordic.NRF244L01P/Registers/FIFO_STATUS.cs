namespace Radio.Nordic.NRF24L01P
{
    public struct FIFO_STATUS : IREGISTER
    {
        public REGISTER_SHORT Register;

        public FIFO_STATUS()
        {
            Register.Id = 0x17;
        }
        public bool TX_REUSE => Register.BIT6;
        public bool TX_FULL => Register.BIT5;
        public bool TX_EMPTY => Register.BIT4;
        public bool RX_FULL => Register.BIT1;
        public bool RX_EMPTY => Register.BIT0;

        public byte Id => Register.Id;

        public int Length => Register.Length;

    }
}