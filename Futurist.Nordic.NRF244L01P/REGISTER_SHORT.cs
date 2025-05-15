namespace Radio.Nordic
{
    public abstract class REGISTER_SHORT : REGISTER
    {
        public REGISTER_SHORT()
        {
            Register = new byte[1];
            Length = 1;
        }
    }
}