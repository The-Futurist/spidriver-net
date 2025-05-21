namespace Radio.Nordic.NRF24L01P
{
    public unsafe struct RF_CH : IREGISTER
    {
        public REGISTER_SHORT Register;

        public RF_CH()
        {
            Register.Id = 0x05;
        }
        public byte CH
        {
            get => Register.Register[0]; set => Register.Register[0] = value;
        }

        public byte Id => Register.Id;

        public int Length => Register.Length;

    }
}