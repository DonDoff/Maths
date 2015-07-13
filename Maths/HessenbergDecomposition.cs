using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class HessenbergDecomposition {
        public Matrix HT { get; private set; }  // Hessenberg transform
        public Matrix HM { get; private set; }  // Hessenberg matrix
        private Matrix M;

        public HessenbergDecomposition(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            if (M.Width > M.Height) {
                throw new MatrixException("The number of rows should be equal or greater than the number of columns!");
            }

            Matrix A = M.Copy();
            Matrix[] Hs = new Matrix[M.Width - 2];

            for (int k = 0; k < Hs.Length; k++) {
                Vector x = A[Vector.Arrange(k + 1, A.Height), k];
                HT = MatrixMath.CalculateHouseholderTransform(x);

                Matrix H_k = Matrix.IdentityMatrix(M.Height);
                for (int i = 0; i < HT.Height; i++) {
                    for (int j = 0; j < HT.Width; j++) {
                        H_k[i + k + 1, j + k + 1] = HT[i, j];
                    }
                }
                Hs[k] = H_k;

                A = H_k * A * H_k.ConjugateTranspose();
            }

            HT = Hs[Hs.Length - 1];
            for (int i = Hs.Length - 2; i >= 0; i--) {
                HT *= Hs[i];
            }

            HM = HT * M * HT.ConjugateTranspose();
        }

    }
}
