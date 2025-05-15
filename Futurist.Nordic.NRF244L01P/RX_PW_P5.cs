namespace Radio.Nordic
{
    public class RX_PW_P5 : REGISTER_SHORT
    {
        public RX_PW_P5()
        {
            Id = 0x16;
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