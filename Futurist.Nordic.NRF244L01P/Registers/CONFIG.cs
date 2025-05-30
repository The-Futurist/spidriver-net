namespace Radio.Nordic.NRF24L01P
{
    public struct CONFIG : IREGISTER
    {
        private REGISTER bits;
        public byte REGID => 0x00;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool RESERVED => bits.BIT7;
        public bool MASK_RX_DR
        {
            get => bits.BIT6; set => bits.BIT6 = value;
        }
        public bool MASK_TX_DS
        {
            get => bits.BIT5; set => bits.BIT5 = value;
        }
        public bool MASK_MAX_RT
        {
            get => bits.BIT4; set => bits.BIT4 = value;
        }
        public bool EN_CRC
        {
            get => bits.BIT3; set => bits.BIT3 = value;
        }
        public bool CRCO
        {
            get => bits.BIT2; set => bits.BIT2 = value;
        }
        public bool PWR_UP
        {
            get => bits.BIT1; set => bits.BIT1 = value;
        }
        public bool PRIM_RX
        {
            get => bits.BIT0; set => bits.BIT0 = value;
        }

        public int LENGTH => 1;

        public override string ToString()
        {
            return $"MASK_RX_DR={MASK_RX_DR} MASK_TX_DS={MASK_TX_DS} MASK_MAX_RT={MASK_MAX_RT} MASK_EN_CRC={EN_CRC} CRCO={CRCO} PWR_UP={PWR_UP} PRIM_RX={PRIM_RX}";
        }
    }
}