using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futurist.Nordic.NRF244L01P
{
    public enum OutputPower : byte
    {
        Min = 0,  // -18dBm
        Low = 1,  // -12dBm
        High = 2, // -6dBm
        Max = 3   // 0dBm
    }
}
