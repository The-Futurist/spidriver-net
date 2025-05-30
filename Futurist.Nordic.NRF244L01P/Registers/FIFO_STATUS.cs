namespace Radio.Nordic.NRF24L01P
{
    public struct FIFO_STATUS : IREGISTER
    {
        private REGISTER bits;
        public byte REGID => 0x17;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool TX_REUSE => bits.BIT6;
        public bool TX_FULL => bits.BIT5;
        public bool TX_EMPTY => bits.BIT4;
        public bool RX_FULL => bits.BIT1;
        public bool RX_EMPTY =>  bits.BIT0;

        public int LENGTH => 1;
    }
}