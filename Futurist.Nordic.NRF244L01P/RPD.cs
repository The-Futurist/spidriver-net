namespace Radio.Nordic
{
    public class RPD : REGISTER_SHORT
    {
        public RPD()
        {
            Id = 0x09;
        }
        public bool CD_0
        {
            get
            {
                return (Register[0] & 0x01) != 0;
            }
        }
    }
}