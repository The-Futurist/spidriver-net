namespace Radio.Nordic
{
    public class RX_ADDR_P5 : REGISTER_SHORT
    {

        public RX_ADDR_P5()
        {
            Id = 0x0F;
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