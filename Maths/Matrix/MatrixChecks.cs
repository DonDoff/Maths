using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public static class MatrixChecks {
        public static bool IsSquare(this Matrix m) {
            return m.Height == m.Width;
        }

        public static bool IsReal(this Matrix m) {
            return m.CheckAllElements(x => x.IsReal());
        }

        public static bool IsPureImaginary(this Matrix m) {
            return m.CheckAllElements(x => x.IsPureImaginary());
        }

        public static bool IsSymmetric(this Matrix m) {
            return m.IsSquare() && m == m.Transpose();
        }

        public static bool IsHermitian(this Matrix m) {
            return m.IsSquare() && m == m.ConjugateTranspose();
        }

        public static bool IsDiagonal(this Matrix m) {
            for (int i = 0; i < m.Height; i++) {
                for (int j = 0; j < m.Width; j++) {
                    if (i != j && m[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsBiDiagonal(this Matrix m) {
            for (int i = 0; i < m.Height; i++) {
                for (int j = 0; j < m.Width; j++) {
                    if (!(i == j || j == i + 1) && m[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsUpperTriangular(this Matrix m) {
            for (int i = 0; i < m.Height; i++) {
                for (int j = 0; j < i; j++) {
                    if (m[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsLowerTriangular(this Matrix m) {
            return m.Transpose().IsUpperTriangular();
        }

        public static bool IsUpperHessenberg(this Matrix m) {
            for (int i = 0; i < m.Height; i++) {
                for (int j = 0; j < i - 1; j++) {
                    if (m[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsUnitary(this Matrix m) {
            return m.IsSquare() && (m.ConjugateTranspose() * m) == MatrixFactory.IdentityMatrix(m.Height);
        }

        public static bool IsOrthogonal(this Matrix m) {
            return m.IsSquare() && (m.Transpose() * m) == MatrixFactory.IdentityMatrix(m.Height);
        }

        public static bool IsPositiveSemiDefinite(this Matrix m) {
            EigenvalueDecomposition eig = m.Eigen();
            return eig.Eigenvalues.CheckAllElements(x => x < 0);
        }

        public static bool IsPositiveDefinite(this Matrix m) {
            //EigenvalueDecomposition eig = m.Eigen();
            //return eig.Eigenvalues.CheckAllElements(x => x < 0);
            try {
                m.Chol();
                return true;
            } catch (MatrixException) {
                return false;
            }
        }

        public static bool IsSingular(this Matrix m) {
            return m.Determinant() == 0;
        }
    }
}
