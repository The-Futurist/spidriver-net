namespace Radio.Nordic.NRF24L01P
{
    public class CONFIG : REGISTER_SHORT
    {
        public CONFIG()
        {
            Id = 0x00;
        }
        public bool RESERVED => BIT7;
        public bool MASK_RX_DR
        {
            get => BIT6; set => BIT6 = value;
        }
        public bool MASK_TX_DS
        {
            get => BIT5; set => BIT5 = value;
        }
        public bool MASK_MAX_RT
        {
            get => BIT4; set => BIT4 = value;
        }
        public bool EN_CRC
        {
            get => BIT3; set => BIT3 = value;
        }
        public bool CRCO
        {
            get => BIT2; set => BIT2 = value;
        }
        public bool PWR_UP
        {
            get => BIT1; set => BIT1 = value;
        }
        public bool PRIM_RX
        {
            get => BIT0; set => BIT0 = value;
        }
        public override string ToString()
        {
            return $"MASK_RX_DR={MASK_RX_DR} MASK_TX_DS={MASK_TX_DS} MASK_MAX_RT={MASK_MAX_RT} MASK_EN_CRC={EN_CRC} CRCO={CRCO} PWR_UP={PWR_UP} PRIM_RX={PRIM_RX}";
        }
    }
}