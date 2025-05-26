namespace Radio.Nordic.NRF24L01P
{
    public class RX_ADDR_P3 : IREGISTER
    {
        public REGISTER_SHORT Register;

        public RX_ADDR_P3()
        {
            Register.Id = 0x0D;
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