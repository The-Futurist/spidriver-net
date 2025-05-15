namespace Radio.Nordic
{
    public class RX_PW_P3 : REGISTER_SHORT
    {
        public RX_PW_P3()
        {
            Id = 0x14;
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