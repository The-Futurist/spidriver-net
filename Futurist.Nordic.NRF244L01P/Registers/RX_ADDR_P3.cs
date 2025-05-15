namespace Radio.Nordic.NRF24L01P
{
    public class RX_ADDR_P3 : REGISTER_SHORT
    {

        public RX_ADDR_P3()
        {
            Id = 0x0D;
        }
        public byte ADDR
        {
            get
            {
                return Register[0];
            }
            set
            {
                Register[0] = value;
            }
        }
    }
}