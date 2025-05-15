namespace Radio.Nordic
{
    public class RX_PW_P4 : REGISTER_SHORT
    {
        public RX_PW_P4()
        {
            Id = 0x15;
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