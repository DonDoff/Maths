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
                for (int j = 0; j < i && j < m.Width; j++) {
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
            if (!m.IsSquare()) {
                return false;
            }
            for (int i = 0; i < m.Height; i++) {
                for (int j = 0; j < i - 1; j++) {
                    if (m[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        ///// <summary>
        ///// This function checks if the matrix is column wise unitary.
        ///// </summary>
        ///// <param name="m"></param>
        ///// <returns></returns>
        //public static bool IsColumnUnitary(this Matrix m) {
        //    return (m.ConjugateTranspose() * m) == MatrixFactory.IdentityMatrix(m.Width);
        //}

        ///// <summary>
        ///// This function checks if the matrix is column wise unitary, up until the given column.
        ///// </summary>
        ///// <param name="m"></param>
        ///// <param name="checkUntilColumn"></param>
        ///// <returns></returns>
        //public static bool IsColumnUnitary(this Matrix m, int checkUntilColumn) {
        //    Matrix reduced = (m.ConjugateTranspose() * m).SubMatrix(0, checkUntilColumn, 0, checkUntilColumn);

        //    return reduced == MatrixFactory.IdentityMatrix(reduced.Width);
        //}

        //public static bool IsRowUnitary(this Matrix m) {
        //    return (m * m.ConjugateTranspose()) == MatrixFactory.IdentityMatrix(m.Height);
        //}

        public static bool IsUnitary(this Matrix m) {
            return m.IsSquare() && (m.ConjugateTranspose() * m) == MatrixFactory.IdentityMatrix(m.Height);
        }

        public static bool IsOrthogonal(this Matrix m) {
            return m.IsSquare() && (m.Transpose() * m) == MatrixFactory.IdentityMatrix(m.Height);
        }

        public static bool IsPositiveSemiDefinite(this Matrix m) {
            //EigenvalueDecomposition eig = m.Eigen();
            //return eig.Eigenvalues.CheckAllElements(x => x < 0);
            try {
                m.Chol();
                return true;
            } catch (MatrixException) {
                return false;
            }
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
