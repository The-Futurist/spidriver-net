namespace Radio.Nordic.NRF24L01P
{
    public class RX_ADDR_P2 : REGISTER_SHORT
    {

        public RX_ADDR_P2()
        {
            Id = 0x0C;
        }
        public byte ADDR
        {
            get => Register[0]; set => Register[0] = value;
        }
    }
}