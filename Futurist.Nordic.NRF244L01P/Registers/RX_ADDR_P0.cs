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
            get
            {
                //return Register.Register;

                Span<byte> buffer = new Span<byte>(ref Register.Register[0], 5);
            }

            set => Register.Register = value;
        }

        public byte Id => Register.Id;

        public int Length => Register.Length;

    }
}