namespace Radio.Nordic.NRF24L01P
{
    public struct FIFO_STATUS : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x17;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool TX_REUSE => bits[6];
        public bool TX_FULL => bits[5];
        public bool TX_EMPTY => bits[4];
        public bool RX_FULL => bits[1];
        public bool RX_EMPTY =>  bits[0];

        public int LENGTH => 1;
    }
}