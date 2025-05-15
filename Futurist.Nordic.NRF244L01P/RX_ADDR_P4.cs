namespace Radio.Nordic
{
    public class RX_ADDR_P4 : REGISTER_SHORT
    {

        public RX_ADDR_P4()
        {
            Id = 0x0E;
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