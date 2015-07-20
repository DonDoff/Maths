using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    class MedianFilter : IFilter {
        public Vector Data { get; private set; }
        public int WindowSize { get; set; }

        public MedianFilter(Vector data, int windowSize = 5) {
            Data = data.Copy();
            WindowSize = windowSize;
        }

        public IFilter Filter() {
            int halfWindowSize = WindowSize / 2;
            for (int i = halfWindowSize; i < Data.Size - halfWindowSize; i++) {
                IList<Complex> windowedData = new List<Complex>(WindowSize);
                for (int j = 0; j < WindowSize; j++) {
                    windowedData.Add(Data[i + j - halfWindowSize]);
                }
                windowedData = windowedData.OrderBy(o => o.Abs()).ToList();
                Data[i] = windowedData[halfWindowSize];
            }

            for (int i = 0; i < halfWindowSize; i++) {
                Data[i] = Data[halfWindowSize];
                Data[Data.Size - i - 1] = Data[Data.Size - halfWindowSize - 1];
            }
            return this;
        }
    }
}
