using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class MatrixMath {
        //////////////////////////////////////////////////////
        //               Matrix Operations                  //
        //////////////////////////////////////////////////////

        public static Matrix Sin(Matrix m) {
            return m.AppyToAllElements(x => ComplexMath.Sin(x));
        }

        public static Matrix Cos(Matrix m) {
            return m.AppyToAllElements(x => ComplexMath.Cos(x));
        }

        public static Matrix Sinh(Matrix m) {
            return m.AppyToAllElements(x => ComplexMath.Sinh(x));
        }

        public static Matrix Cosh(Matrix m) {
            return m.AppyToAllElements(x => ComplexMath.Cosh(x));
        }

        public static Matrix Sinc(Matrix m) {
            return m.AppyToAllElements(x => ComplexMath.Sinc(x));
        }

        public static Complex Min(Matrix m) {
            Complex min = Complex.MAX;
            for (int i = 0; i < m.Height; i++) {
                for (int j = 0; j < m.Width; j++) {
                    if (m[i, j] < min) {
                        min = m[i, j];
                    }
                }
            }
            return min;
        }

        public static Complex Max(Matrix m) {
            Complex max = Complex.MIN;
            for (int i = 0; i < m.Height; i++) {
                for (int j = 0; j < m.Width; j++) {
                    if (m[i, j] > max) {
                        max = m[i, j];
                    }
                }
            }
            return max;
        }

        public static Matrix Exp(Matrix m) {
            return m.AppyToAllElements(x => ComplexMath.Exp(x));
        }

        public static Matrix Sqrt(Matrix m) {
            return m.AppyToAllElements(x => ComplexMath.Sqrt(x));
        }

        public static Matrix Abs(Matrix m) {
            return m.AppyToAllElements(x => ComplexMath.Abs(x));
        }

        public static Matrix CalculateHouseholderTransform(Vector x) {
            if (x.EuclidianNorm() == 0) {
                return MatrixFactory.IdentityMatrix(x.Height);
            }

            double a = x[0].R == 0 ? x.EuclidianNorm() : Math.Sign(x[0].R) * x.EuclidianNorm();
            Vector u = x + a * Vector.Ei(x.Size, 0);
            Vector v = u / u.EuclidianNorm();

            Complex w = (x.ConjugateTranspose() * v).ToComplexNumber() / (v.ConjugateTranspose() * x).ToComplexNumber();
            return (MatrixFactory.IdentityMatrix(x.Height) - (1.0 + w) * v * v.ConjugateTranspose());
        }

        public static Matrix CalculatGivensRotation(Complex a, Complex b) {
            Complex c, s, r;

            r = ComplexMath.Sqrt(a.Conjugate() * a + b.Conjugate() * b);

            if (b == 0) {
                c = 1;
                s = 0;
            } else if (a == 0) {
                c = 0;
                s = ComplexMath.Sign(b.Conjugate());
            } else {
                c = a.Abs() / r;
                s = ComplexMath.Sign(a) * b.Conjugate() / r;
            }

            Matrix givensRot = new Matrix(2);
            givensRot[0, 0] = c;
            givensRot[0, 1] = s;
            givensRot[1, 0] = -s.Conjugate();
            givensRot[1, 1] = c;

            return givensRot;
        }

        /// <summary>
        /// Given a vector space defined by the columns of the matrix x, an orthonormal projection y is calculated using the Gram-Schmidt process.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Matrix GramSchmidthProcess(Matrix x) {
            Matrix y = MatrixFactory.Zeros(x.Height, x.Width);
            y[Vector.Arrange(y.Height), 0] = x[Vector.Arrange(x.Height), 0];

            for (int i = 1; i < x.Width; i++) {
                Vector xi = x[Vector.Arrange(x.Height), i];
                Vector projection = new Vector(xi.Size);

                for (int j = 0; j < i - 1; j++) {
                    Vector yj = y[Vector.Arrange(y.Height), j];
                    projection += xi.DotProduct(yj) / yj.DotProduct(yj) * yj;
                }

                y[Vector.Arrange(y.Height), i] = x[Vector.Arrange(x.Height), i] - projection;
            }
            return y.NormalizeColumns();
        }

    }
}
