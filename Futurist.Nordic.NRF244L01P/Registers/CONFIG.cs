namespace Radio.Nordic.NRF24L01P
{
    public struct CONFIG : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x00;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool RESERVED => bits[7];
        public CONFIG()
        {
        }
        public bool MASK_RX_DR
        {
            get => bits[6]; set => bits[6] = value;
        }
        public bool MASK_TX_DS
        {
            get => bits[5]; set => bits[5] = value;
        }
        public bool MASK_MAX_RT
        {
            get => bits[4]; set => bits[4] = value;
        }
        public bool EN_CRC
        {
            get => bits[3]; set => bits[3] = value;
        }
        public bool CRCO
        {
            get => bits[2]; set => bits[2] = value;
        }
        public bool PWR_UP
        {
            get => bits[1]; set => bits[1] = value;
        }
        public bool PRIM_RX
        {
            get => bits[0]; set => bits[0] = value;
        }

        public int LENGTH => 1;

        public override string ToString()
        {
            return $"MASK_RX_DR={MASK_RX_DR} MASK_TX_DS={MASK_TX_DS} MASK_MAX_RT={MASK_MAX_RT} MASK_EN_CRC={EN_CRC} CRCO={CRCO} PWR_UP={PWR_UP} PRIM_RX={PRIM_RX}";
        }
    }
}