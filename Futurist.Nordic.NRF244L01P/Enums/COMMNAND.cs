namespace Radio.Nordic.NRF24L01P
{
    public enum COMMNAND : byte
    {
        R_REGISTER = 0b0000_0000,
        W_REGISTER = 0b0010_0000,
        R_RX_PAYLOAD = 0b0110_0001,
        W_TX_PAYLOAD = 0b1010_0000,
        FLUSH_TX = 0b1110_0001,
        FLUSH_RX = 0b1110_0010,
        REUSE_TX_PL = 0b1110_0011,
        R_RX_PL_WID = 0b0110_0000,
        W_ACK_PAYLOAD = 0b1010_1000,
        W_TX_PAYLOAD_NO_ACK = 0b1011_0000,
        NOP = 0b11111111,
    }
}