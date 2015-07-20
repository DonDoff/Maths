using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    // Based on: https://math.dartmouth.edu/~m56s13/Southworth_proj.pdf
    public class SVD : IDecomposition {
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
            Matrix B = bid.B.Copy();

            D = B.ConjugateTranspose() * B;

            if (D.Height > 1) {
                V = EigenDecomp();
                V = bid.V * V;

                Matrix CopyOfD = D.Copy();
                D = MatrixFactory.IdentityMatrix(D.Height);
                for (int i = 0; i < D.Height; i++) {
                    D[i, i] = ComplexNumberMath.Sqrt(CopyOfD[i, i]);
                }

                U = new Matrix(M.Height, V.Width);
                for (int i = 0; i < V.Width; i++) {
                    U[Vector.Arrange(M.Height), i] = (M * V[Vector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
                }
            } else {
                V = bid.V;
                D = MatrixMath.Sqrt(D);
                U = new Matrix(M.Height, V.Width);
                for (int i = 0; i < V.Width; i++) {
                    U[Vector.Arrange(M.Height), i] = (M * V[Vector.Arrange(V.Height), i] / D[i, i]).ToColumnVector();
                }
            }
        }

        private Matrix EigenDecomp() {
            Matrix shift = MatrixFactory.IdentityMatrix(D.Height) * ComputeShift(D);
            QRDecomposition qr = (D - shift).QR();
            Matrix Eigenvector = qr.Q;

            for (int i = 0; i < ITERATION_DEPTH; i++) {
                D = qr.R * qr.Q + shift;
                shift = MatrixFactory.IdentityMatrix(D.Height) * ComputeShift(D);
                qr = (D - shift).QR();

                Eigenvector *= qr.Q;

                if (D.IsDiagonal()) {
                    Console.WriteLine(i);
                    break;
                }
            }
            return Eigenvector;
        }

        /// <summary>
        /// The shift is generally taken to be the smallest eigenvalue of the 2 × 2 matrix in the bottom.
        /// </summary>
        /// <returns></returns>
        private ComplexNumber ComputeShift(Matrix mat) {
            Matrix subMat = mat.SubMatrix(mat.Height - 2, mat.Height, mat.Width - 2, mat.Width);
            ComplexNumber trace = subMat.Trace();
            ComplexNumber det = subMat.Determinant();
            ComplexNumber eig1 = (trace + ComplexNumberMath.Sqrt(trace * trace - 4 * det)) / 2;
            ComplexNumber eig2 = (trace - ComplexNumberMath.Sqrt(trace * trace - 4 * det)) / 2;
            ComplexNumber eigen = ComplexNumberMath.Min(eig1, eig2);
            return eigen;
        }


        //private Matrix QRSpecialGivens(Matrix B) {
        //    Matrix G;
        //    U = Matrix.IdentityMatrix(M.Height);
        //    V = Matrix.IdentityMatrix(M.Height);

        //    // B.Width > 1!

        //    ComplexNumber shift = ComputeShift(B);
        //    ComplexNumber t11 = B[0, 0] * B[0, 0] - shift;
        //    ComplexNumber t21 = B[0, 0] * B[0, 1];
        //    G = Matrix.IdentityMatrix(M.Height);
        //    G[Vector.Arrange(0, 2), Vector.Arrange(0, 2)] = MatrixMath.CalculatGivensRotation(t11, t21);
        //    B = B * G.ConjugateTranspose();
        //    V = G.ConjugateTranspose();

        //    for (int i = 1; i < B.Height; i++) {

        //        G = Matrix.IdentityMatrix(B.Height);
        //        G[Vector.Arrange(i - 1, i + 1), Vector.Arrange(i - 1, i + 1)] = MatrixMath.CalculatGivensRotation(B[i - 1, i - 1], B[i, i - 1]);
        //        B = G * B;
        //        U = G * U;

        //        if (i < B.Height - 1) {
        //            G = Matrix.IdentityMatrix(B.Height);
        //            G[Vector.Arrange(i, i + 2), Vector.Arrange(i, i + 2)] = MatrixMath.CalculatGivensRotation(B[i - 1, i], B[i - 1, i + 1]);
        //            B = B * G.ConjugateTranspose();
        //            V = V * G.ConjugateTranspose();
        //        }
        //    }
        //    return B;
        //}
    }
}
