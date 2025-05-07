using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.CommandLine
{
    public abstract class  REGISTER
    {
        public byte[] register;
        public byte type;
        public int length;
        
    }
    public abstract class REGISTER_SHORT  : REGISTER
    {
        public REGISTER_SHORT() 
        {
            register = new byte[1];
            length = 1;
        }
    }
    public abstract class REGISTER_LONG : REGISTER
    {
        public REGISTER_LONG()
        {
            register = new byte[5];
            length = 5;
        }
    }
    public class CONFIG  :    REGISTER_SHORT
    {

        public CONFIG()
        {
            type = 0;
        }
        public bool RESERVED
        {
            get
            {
                return (register[0] & 0x80) != 0;
            }
        }

        public bool MASK_RX_DR
        {
            get
            {
                return (register[0] & 0x40) != 0;
            }
        }

        public bool MASK_TX_DS
        {
            get
            {
                return (register[0] & 0x20) != 0;
            }
        }

        public bool MASK_MAX_RT
        {
            get
            {
                return (register[0] & 0x10) != 0;
            }
        }

        public bool MASK_EN_CRC
        {
            get
            {
                return (register[0] & 0x08) != 0;
            }
        }

        public bool CRCO
        {
            get
            {
                return (register[0] & 0x04) != 0;
            }
        }

        public bool PWR_UP
        {
            get
            {
                return (register[0] & 0x02) != 0;
            }
        }

        public bool PRIM_RX
        {
            get
            {
                return (register[0] & 0x01) != 0;
            }
        }
    }
    public class EN_AA  : REGISTER_SHORT
    {
        public EN_AA()
        {
            type = 1;
        }
        public bool ENAA_P0
        {
            get
            {
                return (register[0] & 0x01) != 0;
            }

            set
            {
                if (value)
                {
                    register[0] |= 0x01;
                }
                else
                {
                    register[0] &= 0xFE;
                }
            }

        }
        public bool ENAA_P1
        {
            get
            {
                return (register[0] & 0x02) != 0;
            }

            set
            {
                if (value)
                {
                    register[0] |= 0x02;
                }
                else
                {
                    register[0] &= 0xFD;
                }
            }
        }
        public bool ENAA_P2
        {
            get
            {
                return (register[0] & 0x04) != 0;
            }

            set
            {
                if (value)
                {
                    register[0] |= 0x04;
                }
                else
                {
                    register[0] &= 0xFB;
                }
            }
        }
        public bool ENAA_P3
        {
            get
            {
                return (register[0] & 0x08) != 0;
            }

            set
            {
                if (value)
                {
                    register[0] |= 0x08;
                }
                else
                {
                    register[0] &= 0xF7;
                }
            }
        }
        public bool ENAA_P4
        {
            get
            {
                return (register[0] & 0x10) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x10;
                }
                else
                {
                    register[0] &= 0xEF;
                }
            }
        }
        public bool ENAA_P5
        {
            get
            {
                return (register[0] & 0x20) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x20;
                }
                else
                {
                    register[0] &= 0xDF;
                }
            }
        }

    }
    public class EN_RXADDR : REGISTER_SHORT
    {
        public EN_RXADDR()
        {
            type = 2;
        }
        public bool ERX_P0
        {
            get
            {
                return (register[0] & 0x01) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x01;
                }
                else
                {
                    register[0] &= 0xFE;
                }
            }
        }

        public bool ERX_P1
        {
            get
            {
                return (register[0] & 0x02) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x02;
                }
                else
                {
                    register[0] &= 0xFD;
                }
            }
        }

        public bool ERX_P2
        {
            get
            {
                return (register[0] & 0x04) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x04;
                }
                else
                {
                    register[0] &= 0xFB;
                }
            }
        }

        public bool ERX_P3
        {
            get
            {
                return (register[0] & 0x08) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x08;
                }
                else
                {
                    register[0] &= 0xF7;
                }
            }
        }

        public bool ERX_P4
        {
            get
            {
                return (register[0] & 0x10) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x10;
                }
                else
                {
                    register[0] &= 0xEF;
                }
            }
        }

        public bool ERX_P5
        {
            get
            {
                return (register[0] & 0x20) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x20;
                }
                else
                {
                    register[0] &= 0xDF;
                }
            }
        }
    }
    public class SETUP_AW : REGISTER_SHORT
    {
        public SETUP_AW()
        {
            type = 3;
        }
        public byte SETUP_AW_0
        {
            get
            {
                return (byte)(register[0] & 0x03);
            }
            set
            {
                register[0] &= 0xFC;
                register[0] |= (byte)(value & 0x03);
            }
        }
    }
    public class SETUP_RETR : REGISTER_SHORT
    {
        public SETUP_RETR()
        {
            type = 4;
        }
        public byte ARD
        {
            get
            {
                return (byte)((register[0] & 0xF0) >> 4);
            }
            set
            {
                register[0] &= 0x0F;
                register[0] |= (byte)((value & 0x0F) << 4);
            }
        }
        public byte ARC
        {
            get
            {
                return (byte)(register[0] & 0x0F);
            }
            set
            {
                register[0] &= 0xF0;
                register[0] |= (byte)(value & 0x0F);
            }
        }
    }
    public class RF_CH : REGISTER_SHORT
    {
        public RF_CH()
        {
            type = 5;
        }
        public byte RF_CH_0
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RF_SETUP : REGISTER_SHORT
    {
        public RF_SETUP()
        {
            type = 6;
        }
        public byte RF_PWR
        {
            get
            {
                return (byte)((register[0] & 0x06) >> 1);
            }
            set
            {
                register[0] &= 0xF9;
                register[0] |= (byte)((value & 0x03) << 1);
            }
        }
        public bool LNA_HCURR
        {
            get
            {
                return (register[0] & 0x01) != 0;
            }
        }
    }
    public class STATUS : REGISTER_SHORT
    {
        public STATUS()
        {
            type = 7;
        }
        public bool RX_DR
        {
            get
            {
                return (register[0] & 0x40) != 0;
            }
        }
        public bool TX_DS
        {
            get
            {
                return (register[0] & 0x20) != 0;
            }
        }
        public bool MAX_RT
        {
            get
            {
                return (register[0] & 0x10) != 0;
            }
        }
        public byte RX_P_NO
        {
            get
            {
                return (byte)((register[0] & 0x0E) >> 1);
            }
        }
    }
    public class OBSERVE_TX : REGISTER_SHORT
    {
        public OBSERVE_TX()
        {
            type = 9;
        }
        public byte PLOS_CNT
        {
            get
            {
                return (byte)((register[0] & 0xF0) >> 4);
            }
        }
        public byte ARC_CNT
        {
            get
            {
                return (byte)(register[0] & 0x0F);
            }
        }
    }
    public class RPD : REGISTER_SHORT
    {
        public RPD()
        {
            type = 0x09;
        }
        public bool CD_0
        {
            get
            {
                return (register[0] & 0x01) != 0;
            }
        }
    }
    public class RX_ADDR_P0 : REGISTER_LONG
    {

        public RX_ADDR_P0()
        {
            type = 0x0A;
        }
        public byte[] RX_ADDR_P0_0
        {
            get
            {
                return register;
            }
            set
            {
                register = value;
            }
        }
    }
    public class RX_ADDR_P1 : REGISTER_LONG
    {

        public RX_ADDR_P1()
        {
            type = 0x0B;
        }
        public byte[] RX_ADDR_P0_0
        {
            get
            {
                return register;
            }
            set
            {
                register = value;
            }
        }
    }
    public class RX_ADDR_P2 : REGISTER_SHORT
    {

        public RX_ADDR_P2()
        {
            type = 0x0C;
        }
        public byte RX_ADDR_P0_0
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RX_ADDR_P3 : REGISTER_SHORT
    {

        public RX_ADDR_P3()
        {
            type = 0x0D;
        }
        public byte RX_ADDR_P0_3
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RX_ADDR_P4 : REGISTER_SHORT
    {

        public RX_ADDR_P4()
        {
            type = 0x0E;
        }
        public byte RX_ADDR_P0_4
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RX_ADDR_P5 : REGISTER_SHORT
    {

        public RX_ADDR_P5()
        {
            type = 0x0F;
        }
        public byte RX_ADDR_P0_5
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class TX_ADDR : REGISTER_LONG
    {
        public TX_ADDR()
        {
            type = 0x10;
        }
        public byte[] TX_ADDR_0
        {
            get
            {
                return register;
            }
            set
            {
                register = value;
            }
        }
    }
    public class RX_PW_P0 : REGISTER_SHORT
    {
        public RX_PW_P0()
        {
            type = 0x11;
        }
        public byte RX_PW_P0_0
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RX_PW_P1 : REGISTER_SHORT
    {
        public RX_PW_P1()
        {
            type = 0x12;
        }
        public byte RX_PW_P1_0
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RX_PW_P2 : REGISTER_SHORT
    {
        public RX_PW_P2()
        {
            type = 0x13;
        }
        public byte RX_PW_P1_0
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RX_PW_P3 : REGISTER_SHORT
    {
        public RX_PW_P3()
        {
            type = 0x14;
        }
        public byte RX_PW_P1_0
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RX_PW_P4 : REGISTER_SHORT
    {
        public RX_PW_P4()
        {
            type = 0x15;
        }
        public byte RX_PW_P1_0
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class RX_PW_P5 : REGISTER_SHORT
    {
        public RX_PW_P5()
        {
            type = 0x16;
        }
        public byte RX_PW_P1_0
        {
            get
            {
                return register[0];
            }
            set
            {
                register[0] = value;
            }
        }
    }
    public class FIFO_STATUS : REGISTER_SHORT
    {
        public FIFO_STATUS()
        {
            type = 0x17;
        }
        public bool TX_REUSE
        {
            get
            {
                return (register[0] & 0x40) != 0;
            }
        }
        public bool TX_FULL
        {
            get
            {
                return (register[0] & 0x20) != 0;
            }
        }
        public bool RX_EMPTY
        {
            get
            {
                return (register[0] & 0x10) != 0;
            }
        }
    }
    public class DYNPD : REGISTER_SHORT
    {
        public DYNPD()
        {
            type = 0x1C;
        }
        public bool DPL_P0
        {
            get
            {
                return (register[0] & 0x01) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x01;
                }
                else
                {
                    register[0] &= 0xFE;
                }
            }
        }
        public bool DPL_P1
        {
            get
            {
                return (register[0] & 0x02) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x02;
                }
                else
                {
                    register[0] &= 0xFD;
                }
            }
        }
    }
    public class FEATURE : REGISTER_SHORT
    {
        public FEATURE()
        {
            type = 0x1D;
        }
        public bool EN_DPL
        {
            get
            {
                return (register[0] & 0x04) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x04;
                }
                else
                {
                    register[0] &= 0xFB;
                }
            }
        }
        public bool EN_ACK_PAY
        {
            get
            {
                return (register[0] & 0x02) != 0;
            }
            set
            {
                if (value)
                {
                    register[0] |= 0x02;
                }
                else
                {
                    register[0] &= 0xFD;
                }
            }
        }
    }
}
