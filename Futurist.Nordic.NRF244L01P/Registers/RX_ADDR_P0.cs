namespace Radio.Nordic.NRF24L01P
{
    public struct RX_ADDR_P0 : IREGISTER
    {
        public REGISTER_LONG Register;

        public RX_ADDR_P0()
        {
            Register.Id = 0x0A;
        }
        public byte[] ADDR
        {
            get => Register.BYTES;
            set => Register.BYTES = value;
        }

        public byte Id => Register.Id;

        public int Length => Register.Length;
    }
}