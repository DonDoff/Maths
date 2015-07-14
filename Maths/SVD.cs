using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    // Based on: https://math.dartmouth.edu/~m56s13/Southworth_proj.pdf
    public class SVD {
        private static int ITERATION_DEPTH = 50;

        public Matrix U { get; private set; }
        public Matrix D { get; private set; }
        public Matrix V { get; private set; }
        private Matrix M;

        public SVD(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            Bidiagonalization bid = M.Bidiagonalization();
            Matrix B = bid.B;
            D = B.ConjugateTranspose() * B;

            if (D.Height > 1) {                
                V = EigenDecomp();
                V = bid.V * V;

                //D = B * B.ConjugateTranspose();
                //U = EigenDecomp();
                //U = bid.U * U;
                //D = D.SubMatrix(0, M.Height, 0, M.Width);

                D = MatrixMath.Sqrt(D);

                U = new Matrix(M.Height, V.Width);
                for (int i = 0; i < V.Width; i++) {
                    U[ColumnVector.Arrange(M.Height), i] = (M * V[ColumnVector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
                }
            } else {
                V = bid.V;
                D = MatrixMath.Sqrt(D);
                U = new Matrix(M.Height, V.Width);
                for (int i = 0; i < V.Width; i++) {
                    U[ColumnVector.Arrange(M.Height), i] = (M * V[ColumnVector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
                }
            }
        }

        private Matrix EigenDecomp() {
            Matrix subBTB = D.SubMatrix(D.Height - 2, D.Height, D.Width - 2, D.Width);
            ComplexNumber trace = subBTB.Trace();
            ComplexNumber det = subBTB.Determinant();
            ComplexNumber eig1 = (trace + ComplexNumberMath.Sqrt(trace * trace - 4 * det)) / 2;
            ComplexNumber eig2 = (trace - ComplexNumberMath.Sqrt(trace * trace - 4 * det)) / 2;
            ComplexNumber smallest = ComplexNumberMath.Min(eig1, eig2);
            ComplexNumber eigenSquared = smallest * smallest;
            Matrix shift = eigenSquared * Matrix.IdentityMatrix(D.Height);

            QRDecomposition qr = (D - shift).QR();
            Matrix Eigenvector = qr.Q;

            for (int i = 0; i < ITERATION_DEPTH; i++) {
                D = qr.R * qr.Q + shift;
                qr = (D - shift).QR();

                Eigenvector *= qr.Q;

                if (D.IsDiagonal()) {
                    break;
                }
            }
            return Eigenvector;
        }
    }
}
