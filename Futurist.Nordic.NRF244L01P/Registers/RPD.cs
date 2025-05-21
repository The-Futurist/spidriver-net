namespace Radio.Nordic.NRF24L01P
{
    public struct RPD : IREGISTER
    {
        public REGISTER_SHORT Register;

        public RPD()
        {
            Register.Id = 0x09;
        }
        public bool CD_0 => Register.BIT0;

        public byte Id => Register.Id;

        public int Length => Register.Length;

    }
}