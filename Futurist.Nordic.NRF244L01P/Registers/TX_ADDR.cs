namespace Radio.Nordic.NRF24L01P
{
    public class TX_ADDR : REGISTER_LONG
    {
        public TX_ADDR()
        {
            Id = 0x10;
        }
        public byte[] ADDR
        {
            get
            {
                return Register;
            }
            set
            {
                Register = value;
            }
        }
    }
}