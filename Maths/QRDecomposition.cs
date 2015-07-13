using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class QRDecomposition {

        public Matrix Q { get; private set; }
        public Matrix R { get; private set; }

        private Matrix M;

        public QRDecomposition(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            Matrix[] Qs = new Matrix[Math.Min(M.Height - 1, M.Width)];
            Matrix A = M.Copy();

            for (int k = 0; k < Qs.Length; k++) {
                Vector x = new Vector(A, 0);
                Matrix Q = MatrixMath.CalculateHouseholderTransform(x);
                Matrix QA = Q * A;
                A = QA.SubMatrix(1, A.Height, 1, A.Width);

                Matrix Q_k = Matrix.IdentityMatrix(M.Height);
                for (int i = 0; i < Q.Height; i++) {
                    for (int j = 0; j < Q.Width; j++) {
                        Q_k[i + k, j + k] = Q[i, j];
                    }
                }

                Qs[k] = Q_k;
            }

            // Q
            this.Q = Qs[0];
            for (int i = 1; i < Qs.Length; i++) {
                Q *= Qs[i];
            }

            // R
            R = Q.ConjugateTranspose() * M;
        }
    }
}
