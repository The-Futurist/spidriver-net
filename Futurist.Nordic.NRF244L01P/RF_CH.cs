namespace Radio.Nordic
{
    public class RF_CH : REGISTER_SHORT
    {
        public RF_CH()
        {
            Id = 5;
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