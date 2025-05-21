namespace Radio.Nordic.NRF24L01P
{
    public unsafe struct OBSERVE_TX : IREGISTER
    {
        public REGISTER_SHORT Register;

        public OBSERVE_TX()
        {
            Register.Id = 0x08;
        }
        public byte PLOS_CNT => (byte)((Register.Register[0] & 0xF0) >> 4);
        public byte ARC_CNT => (byte)(Register.Register[0] & 0x0F);

        public byte Id => Register.Id;

        public int Length => Register.Length;

    }
}