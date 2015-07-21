using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    [Flags]
    public enum MatrixTypes {
        Real = 0x01,
        Square = 0x02,
        Symmetric = 0x04,
    }
}
