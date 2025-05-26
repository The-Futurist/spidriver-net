namespace Radio.Nordic.NRF24L01P
{
    public class RX_ADDR_P1 : IREGISTER
    {
        public REGISTER_LONG Register;

        public RX_ADDR_P1()
        {
            Register.Id = 0x0B;
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