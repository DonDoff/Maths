using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    class TriangularSmoothingFilter : IFilter {        
        public ColumnVector Data { get; private set; }

        public TriangularSmoothingFilter(ColumnVector data) {
            Data = data.Copy();
        }

        public IFilter Filter() {
            RectangularSmoothingFilter rFilter = new RectangularSmoothingFilter(Data);
            Data = rFilter.Filter().Filter().Data;
            return rFilter;
        }
    }
}
