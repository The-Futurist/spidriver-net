using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.CommandLine
{
    public struct CONFIG
    {
        public byte register;

        public ref byte[] Buffer
        { get => ref new byte[] { register };
          set 
            { 
              register = value[0]; 
            }
        }

        public bool RESERVED
        {
            get
            {
                return (register & 0x80) != 0;
            }   
        }

        public bool MASK_RX_DR
        {
            get
            {
                return (register & 0x40) != 0;
            }
        }

        public bool MASK_TX_DS
        {
            get
            {
                return (register & 0x20) != 0;
            }
        }

        public bool MASK_MAX_RT
        {
            get
            {
                return (register & 0x10) != 0;
            }
        }

        public bool MASK_EN_CRC
        {
            get
            {
                return (register & 0x08) != 0;
            }
        }

        public bool CRCO
        {
            get
            {
                return (register & 0x04) != 0;
            }
        }

        public bool PWR_UP
        {
            get
            {
                return (register & 0x02) != 0;
            }
        }

        public bool PRIM_RX
        {
            get
            {
                return (register & 0x01) != 0;
            }
        }
    }
}
