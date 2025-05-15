namespace Radio.Nordic.NRF24L01P
{
    public class RF_CH : REGISTER_SHORT
    {
        public RF_CH()
        {
            Id = 0x05;
        }
        public byte CH
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