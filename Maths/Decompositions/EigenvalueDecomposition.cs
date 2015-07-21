using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class EigenvalueDecomposition : IDecomposition {
        private static int ITERATION_DEPTH = 50;

        public Vector Eigenvalues { get; private set; }
        public Matrix Eigenvectors { get; private set; }

        public Matrix M { get; set; }

        public EigenvalueDecomposition(Matrix m) {
            M = m;
            MakeDecomposition();
        }

        private void MakeDecomposition() {
            if (!(M.IsSquare())) {
                throw new MatrixException("The matrix needs to be square to compute the eigenvalues/eigenvectors!");
            }
            //if (!(M.IsReal() && M.IsSymmetric())) {
            //    throw new MatrixException("The matrix needs to be real and symmetric, to reliably compute the eigenvalues/eigenvectors!");
            //}

            HessenbergDecomposition hessen = new HessenbergDecomposition(M);
            Matrix HT = hessen.P;

            Matrix Ai = hessen.H.Copy();
            QRDecomposition qr = Ai.QR();

            Eigenvalues = new Vector(Ai.Width);
            Eigenvectors = qr.Q;

            for (int i = 0; i < ITERATION_DEPTH; i++) {
                Ai = qr.R * qr.Q;
                qr = Ai.QR();

                Eigenvectors *= qr.Q;

                if (Ai.IsUpperTriangular()) {
                    break;
                }
            }

            for (int j = 0; j < Ai.Height; j++) {
                Eigenvalues[j] = Ai[j, j];
            }
        }
    }
}
