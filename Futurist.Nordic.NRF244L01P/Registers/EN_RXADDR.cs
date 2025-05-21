namespace Radio.Nordic.NRF24L01P
{
    public struct EN_RXADDR : IREGISTER
    {
        public REGISTER_SHORT Register;

        public EN_RXADDR()
        {
            Register.Id = 0x02;
        }
        public bool ERX_P0
        {
            get => Register.BIT0; set => Register.BIT0 = value;
        }
        public bool ERX_P1
        {
            get => Register.BIT1; set => Register.BIT1 = value;
        }
        public bool ERX_P2
        {
            get => Register.BIT2; set => Register.BIT2 = value;
        }
        public bool ERX_P3
        {
            get => Register.BIT3; set => Register.BIT3 = value;
        }
        public bool ERX_P4
        {
            get => Register.BIT4; set => Register.BIT4 = value;
        }
        public bool ERX_P5
        {
            get => Register.BIT5; set => Register.BIT5 = value;
        }

        public byte Id => Register.Id;

        public int Length => Register.Length;

    }
}