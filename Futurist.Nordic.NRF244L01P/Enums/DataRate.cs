using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futurist.Nordic.NRF244L01P
{
    public enum DataRate : byte
    {
        Min = 2, // 250 kbps
        Med = 0, // 1Mbps
        Max = 1  // 2Mbps
    }
}
