namespace Radio.Nordic.NRF24L01P
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