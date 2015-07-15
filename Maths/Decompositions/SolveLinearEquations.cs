using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class SolveLinearEquations {

        private Matrix M;

        /// <summary>
        /// Tries to find a linear least squares solution to Ax - b.
        /// </summary>
        /// <param name="m"></param>
        public SolveLinearEquations(Matrix m) {
            M = m;
        }

        public Matrix Solve(Matrix b) {
            Matrix x = new Matrix(M.Height, b.Width);

            Vector wholeCollumnIdx = Vector.Arrange(M.Height);
            for (int i = 0; i < b.Width; i++) {
                x[wholeCollumnIdx, i] = Solve(b[wholeCollumnIdx, i]);
            }

            return x;
        }

        public Vector Solve(Vector b) {
            if (M.Height != b.Height) {
                throw new MatrixException("Wrong number of results in solution vector!");
            }

            Vector x = new Vector(M.Height);

            if (M.IsSquare()) {
                // use LU decomposition
                LUDecomposition lu = M.LU();
                Vector bp = (lu.P * b).ToColumnVector();

                // Solves Ly = b using forward substitution
                Vector y = ForwardSubstitution(lu.L, bp);

                // Solves Ux = y using back substitution
                x = BackSubstitution(lu.U, y);
            } else {
                // use QR decomposition
                // TODO: Only works if M is full rank

                // overdetermined system
                if (M.Height >= M.Width) {
                    QRDecomposition qr = M.QR();
                    Matrix R1 = qr.R.SubMatrix(0, M.Width, 0, M.Width);

                    // Solves R1*x = Q.T*b using back substitution
                    x = BackSubstitution(R1, (qr.Q.ConjugateTranspose() * b).ToColumnVector());
                } else {
                    // underdetermined system
                    QRDecomposition qr = M.Transpose().QR(); // note the transpose here
                    Matrix R1 = qr.R.SubMatrix(0, M.Height, 0, M.Height);
                    Matrix Q1 = qr.Q.SubMatrix(0, M.Width, 0, M.Height);

                    // Multiplies Q1 with the solution of R1.T*x = b using forward substitution
                    x = (Q1 * ForwardSubstitution(R1.ConjugateTranspose(), b)).ToColumnVector();
                }
            }

            return x;
        }

        /// <summary>
        /// Matrix A needs to be lower triangular
        /// </summary>
        /// <param name="A"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Vector ForwardSubstitution(Matrix A, Vector b) {
            Vector x = new Vector(A.Height);
            for (int i = 0; i < A.Height; i++) {
                x[i] = b[i];
                for (int j = 0; j < i; j++) {
                    x[i] -= A[i, j] * x[j];
                }
                x[i] = x[i] / A[i, i];
            }
            return x;
        }

        /// <summary>
        /// Matrix A needs to be upper triangular
        /// </summary>
        /// <param name="A"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Vector BackSubstitution(Matrix A, Vector b) {
            Vector x = new Vector(A.Height);
            for (int i = A.Height - 1; i > -1; i--) {
                x[i] = b[i];
                for (int j = A.Width - 1; j > i; j--) {
                    x[i] -= A[i, j] * x[j];
                }
                x[i] = x[i] / A[i, i];
            }
            return x;
        }

    }
}
