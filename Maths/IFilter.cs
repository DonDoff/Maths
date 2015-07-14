using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    interface IFilter {
        IFilter Filter();
        ColumnVector Data { get; }
    }
}
