using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class QRDecomposition : IDecomposition {

        public Matrix Q { get; private set; }
        public Matrix R { get; private set; }

        private Matrix M;

        public QRDecomposition(Matrix m) {
            M = m;
            MakeDecomposition();
            //MakeDecompositionUsingGivens();
        }

        private void MakeDecomposition() {
            Matrix A = M.Copy();

            Matrix[] Qs = new Matrix[Math.Min(M.Height - 1, M.Width)];

            for (int k = 0; k < Qs.Length; k++) {
                Vector x = new Vector(A, 0);
                Matrix Q = MatrixMath.CalculateHouseholderTransform(x);
                Matrix QA = Q * A;
                A = QA.SubMatrix(1, A.Height, 1, A.Width);

                Qs[k] = Matrix.IdentityMatrix(M.Height);
                Qs[k][Vector.Arrange(k, M.Height), Vector.Arrange(k, M.Height)] = Q;
            }

            if (Qs.Length == 0) {
                Q = Matrix.IdentityMatrix(M.Height);
            } else {
                Q = Qs[0];
                for (int i = 1; i < Qs.Length; i++) {
                    Q *= Qs[i];
                }
            }

            R = Q.ConjugateTranspose() * M;
        }

        private void MakeDecompositionUsingGivens() {
            Matrix A = M.Copy();
            Matrix G;
            Q = Matrix.IdentityMatrix(M.Height);

            for (int j = 0; j < M.Width; j++) {
                for (int i = M.Height - 1; i > j; i--) {
                    if (A[i, j] != 0) {
                        G = Matrix.IdentityMatrix(M.Height);
                        //Vector.Arrange(i - 1, i + 1);
                        G[Vector.Arrange(i - 1, i + 1), Vector.Arrange(i - 1, i + 1)] = MatrixMath.CalculatGivensRotation(A[i - 1, j], A[i, j]);

                        A = G * A;
                        Q *= G.ConjugateTranspose();
                    }
                }
            }
            R = A;
        }

    }
}
