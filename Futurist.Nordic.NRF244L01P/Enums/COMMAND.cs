namespace Radio.Nordic.NRF24L01P
{
    public static class COMMAND
    {
        public const byte R_REGISTER = 0b0000_0000;
        public const byte W_REGISTER = 0b0010_0000;
        public const byte R_RX_PAYLOAD = 0b0110_0001;
        public const byte W_TX_PAYLOAD = 0b1010_0000;
        public const byte FLUSH_TX = 0b1110_0001;
        public const byte FLUSH_RX = 0b1110_0010;
        public const byte REUSE_TX_PL = 0b1110_0011;
        public const byte R_RX_PL_WID = 0b0110_0000;
        public const byte W_ACK_PAYLOAD = 0b1010_1000;
        public const byte W_TX_PAYLOAD_NO_ACK = 0b1011_0000;
        public const byte NOP = 0b11111111;
    }
}