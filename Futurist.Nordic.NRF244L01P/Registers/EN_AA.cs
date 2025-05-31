namespace Radio.Nordic.NRF24L01P
{
    public struct EN_AA : IRegister  
    {
        private REGISTER bits;
        public byte REGID => 0x01;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool ENAA_P0
        { get => bits.BIT0; set => bits.BIT0 = value;
        }
        public bool ENAA_P1
        { get => bits.BIT1; set => bits.BIT1 = value;
        }
        public bool ENAA_P2
        { get => bits.BIT2; set => bits.BIT2 = value;
        }
        public bool ENAA_P3
        { get => bits.BIT3; set => bits.BIT3 = value;
        }
        public bool ENAA_P4
        { get => bits.BIT4; set => bits.BIT4 = value;
        }
        public bool ENAA_P5
        { get => bits.BIT5; set => bits.BIT5 = value;
        }

        public int LENGTH => 1;
    }
}