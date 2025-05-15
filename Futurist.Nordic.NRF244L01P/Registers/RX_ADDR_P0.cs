namespace Radio.Nordic.NRF24L01P
{
    public class RX_ADDR_P0 : REGISTER_LONG
    {

        public RX_ADDR_P0()
        {
            Id = 0x0A;
        }
        public byte[] ADDR
        {
            get => Register; set => Register = value;
        }
    }
}