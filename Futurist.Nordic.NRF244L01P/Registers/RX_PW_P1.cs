namespace Radio.Nordic.NRF24L01P
{
    public class RX_PW_P1 : REGISTER_SHORT
    {
        public RX_PW_P1()
        {
            Id = 0x12;
        }
        public byte RX_PW
        {
            get => Register[0]; set => Register[0] = value;
        }
    }
}