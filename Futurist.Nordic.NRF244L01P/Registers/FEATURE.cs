namespace Radio.Nordic.NRF24L01P
{
    public struct FEATURE : IREGISTER
    {
        public REGISTER_SHORT Register;

        public FEATURE()
        {
            Register.Id = 0x1D;
        }
        public bool EN_DYN_ACK
        {
            get => Register.BIT0; set => Register.BIT0 = value;
        }
        public bool EN_ACK_PAY
        {
            get => Register.BIT1; set => Register.BIT1 = value;
        }
        public bool EN_DPL
        {
            get => Register.BIT2; set => Register.BIT2 = value;
        }

        public byte Id => Register.Id;

        public int Length => Register.Length;

    }
}