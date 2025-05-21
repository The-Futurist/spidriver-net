namespace Radio.Nordic.NRF24L01P
{
    public struct DYNPD : IREGISTER
    {
        public REGISTER_SHORT Register;
        public DYNPD()
        {
            Register.Id = 0x1C;
        }
        public bool DPL_P0
        {
            get => Register.BIT0; set => Register.BIT0 = value;
        }
        public bool DPL_P1
        {
            get => Register.BIT1; set => Register.BIT1 = value;
        }
        public bool DPL_P2
        {
            get => Register.BIT2; set => Register.BIT2 = value;
        }
        public bool DPL_P3
        {
            get => Register.BIT3; set => Register.BIT3 = value;
        }
        public bool DPL_P4
        {
            get => Register.BIT4; set => Register.BIT4 = value;
        }
        public bool DPL_P5
        {
            get => Register.BIT5; set => Register.BIT5 = value;
        }

        public byte Id => Register.Id;

        public int Length => Register.Length;
    }
}