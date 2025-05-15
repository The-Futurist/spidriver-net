namespace Radio.Nordic
{
    public class RX_ADDR_P1 : REGISTER_LONG
    {

        public RX_ADDR_P1()
        {
            Id = 0x0B;
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