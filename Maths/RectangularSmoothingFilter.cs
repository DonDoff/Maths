using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    class RectangularSmoothingFilter : IFilter {
        public ColumnVector Data { get; private set; }

        public RectangularSmoothingFilter(ColumnVector data) {
            Data = data.Copy();
        }

        public IFilter Filter() {
            for (int i = 1; i < Data.Size - 1; i++) {
                Data[i] = (Data[i - 1] + Data[i] + Data[i + 1]) / 3;
            }
            return this;
        }
    }
}
