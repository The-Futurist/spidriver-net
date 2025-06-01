using System.IO.Pipelines;
using static Radio.Nordic.NRF24L01P.Pipe;

namespace Radio.Nordic.NRF24L01P
{
    public struct EN_AA : IRegister  
    {
        private REGISTER bits;
        public byte REGID => 0x01;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public bool ENAA_P0
        { get => bits[0]; set => bits[0] = value;
        }
        public bool ENAA_P1
        { get => bits[1]; set => bits[1] = value;
        }
        public bool ENAA_P2
        { get => bits[2]; set => bits[2] = value;
        }
        public bool ENAA_P3
        { get => bits[3]; set => bits[3] = value;
        }
        public bool ENAA_P4
        { get => bits[4]; set => bits[4] = value;
        }
        public bool ENAA_P5
        { get => bits[5]; set => bits[5] = value;
        }

        public bool this[Pipe Index]
        {
            get
            {
                return Index switch
                {
                    Pipe_0 => ENAA_P0,
                    Pipe_1 => ENAA_P1,
                    Pipe_2 => ENAA_P2,
                    Pipe_3 => ENAA_P3,
                    Pipe_4 => ENAA_P4,
                    Pipe_5 => ENAA_P5,
                    _ => throw new NotImplementedException(),
                };
            }
            set
            {
                switch (Index)
                {
                    case Pipe_0: ENAA_P0 = value; break;
                    case Pipe_1: ENAA_P1 = value; break;
                    case Pipe_2: ENAA_P2 = value; break;
                    case Pipe_3: ENAA_P3 = value; break;
                    case Pipe_4: ENAA_P4 = value; break;
                    case Pipe_5: ENAA_P5 = value; break;
                }
            }
        }

        public int LENGTH => 1;
    }
}