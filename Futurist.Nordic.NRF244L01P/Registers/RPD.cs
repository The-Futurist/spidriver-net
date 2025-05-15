namespace Radio.Nordic.NRF24L01P
{
    public class RPD : REGISTER_SHORT
    {
        public RPD()
        {
            Id = 0x09;
        }
        public bool CD_0 => BIT0;
    }
}