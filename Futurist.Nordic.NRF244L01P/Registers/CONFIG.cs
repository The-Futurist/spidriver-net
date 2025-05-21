namespace Radio.Nordic.NRF24L01P
{
    public struct CONFIG : IREGISTER
    {
        public REGISTER_SHORT Register;

        public CONFIG()
        {
            Register.Id = 0x00;
        }
        public bool RESERVED => Register.BIT7;
        public bool MASK_RX_DR
        {
            get => Register.BIT6; set => Register.BIT6 = value;
        }
        public bool MASK_TX_DS
        {
            get => Register.BIT5; set => Register.BIT5 = value;
        }
        public bool MASK_MAX_RT
        {
            get => Register.BIT4; set => Register.BIT4 = value;
        }
        public bool EN_CRC
        {
            get => Register.BIT3; set => Register.BIT3 = value;
        }
        public bool CRCO
        {
            get => Register.BIT2; set => Register.BIT2 = value;
        }
        public bool PWR_UP
        {
            get => Register.BIT1; set => Register.BIT1 = value;
        }
        public bool PRIM_RX
        {
            get => Register.BIT0; set => Register.BIT0 = value;
        }

        public byte Id => Register.Id;

        public int Length => Register.Length;

        public override string ToString()
        {
            return $"MASK_RX_DR={MASK_RX_DR} MASK_TX_DS={MASK_TX_DS} MASK_MAX_RT={MASK_MAX_RT} MASK_EN_CRC={EN_CRC} CRCO={CRCO} PWR_UP={PWR_UP} PRIM_RX={PRIM_RX}";
        }
    }
}