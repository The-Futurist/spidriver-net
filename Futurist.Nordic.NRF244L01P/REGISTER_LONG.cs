namespace Radio.Nordic
{
    public abstract class REGISTER_LONG : REGISTER
    {
        public REGISTER_LONG()
        {
            Register = new byte[5];
            Length = 5;
        }
    }
}