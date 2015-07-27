using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class Bidiagonalization : IDecomposition {
        public Matrix U { get; private set; }
        public Matrix B { get; private set; }
        public Matrix V { get; private set; }

        private Matrix M;

        public Bidiagonalization(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            Matrix A = M.Copy();
            Matrix P, Q;
            U = MatrixFactory.IdentityMatrix(M.Height);
            V = MatrixFactory.IdentityMatrix(M.Width);

            int USize;
            int VSize;

            // Create correct matrix sizes
            if (M.Width == 1) {
                USize = M.Width;
                VSize = 0;
            } else if (M.Height == 1) {
                USize = 0;
                VSize = M.Height;
            } else {
                if (M.Height > M.Width) {
                    USize = M.Width;
                } else {
                    USize = M.Height - 1;
                }

                if (M.Height >= M.Width) {
                    VSize = M.Width - 2;
                } else if (M.Height == M.Width - 1) {
                    VSize = M.Height - 1;
                } else {
                    VSize = M.Height;
                }
            }

            for (int k = 0; k < Math.Max(USize, VSize); k++) {
                // Left-hand reduction
                if (k < USize) {
                    Vector x = new Vector(A, 0);
                    P = MatrixMath.CalculateHouseholderTransform(x);

                    A = (P * A).SubMatrix(0, A.Height, 1, A.Width);

                    Matrix U_k = MatrixFactory.IdentityMatrix(M.Height);
                    U_k[Vector.Arrange(k, U_k.Height), Vector.Arrange(k, U_k.Width)] = P;

                    U *= U_k.ConjugateTranspose();
                } else {
                    A = A.SubMatrix(0, A.Height, 1, A.Width);
                }

                // Right-hand reduction
                if (k < VSize) {
                    Vector x = new Vector(A.Transpose(), 0);
                    Q = MatrixMath.CalculateHouseholderTransform(x).Transpose();
                    A = (A * Q).SubMatrix(1, A.Height, 0, A.Width);

                    Matrix V_k = MatrixFactory.IdentityMatrix(M.Width);
                    V_k[Vector.Arrange(k + 1, V_k.Height), Vector.Arrange(k + 1, V_k.Width)] = Q;

                    V *= V_k;
                } else {
                    A = A.SubMatrix(1, A.Height, 0, A.Width);
                }
            }

            B = U.ConjugateTranspose() * M * V;
        }
    }
}
