namespace Radio.Nordic.NRF24L01P
{
    public class RX_PW_P0 : REGISTER_SHORT
    {
        public RX_PW_P0()
        {
            Id = 0x11;
        }
        public byte RX_PW
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