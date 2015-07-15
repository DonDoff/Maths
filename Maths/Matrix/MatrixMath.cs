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
            return m.AppyToAllElements(x => ComplexNumberMath.Sin(x));
        }

        public static Matrix Cos(Matrix m) {
            return m.AppyToAllElements(x => ComplexNumberMath.Cos(x));
        }

        public static Matrix Sinh(Matrix m) {
            return m.AppyToAllElements(x => ComplexNumberMath.Sinh(x));
        }

        public static Matrix Cosh(Matrix m) {
            return m.AppyToAllElements(x => ComplexNumberMath.Cosh(x));
        }

        public static Matrix Sinc(Matrix m) {
            return m.AppyToAllElements(x => ComplexNumberMath.Sinc(x));
        }

        public static ComplexNumber Min(Matrix m) {
            ComplexNumber min = ComplexNumber.MAX;
            for (int i = 0; i < m.Height; i++) {
                for (int j = 0; j < m.Width; j++) {
                    if (m[i, j] < min) {
                        min = m[i, j];
                    }
                }
            }
            return min;
        }

        public static ComplexNumber Max(Matrix m) {
            ComplexNumber max = ComplexNumber.MIN;
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
            return m.AppyToAllElements(x => ComplexNumberMath.Exp(x));
        }

        public static Matrix Sqrt(Matrix m) {
            return m.AppyToAllElements(x => ComplexNumberMath.Sqrt(x));
        }

        public static Matrix CalculateHouseholderTransform(Vector x) {
            if (x.EuclidianNorm() == 0) {
                return Matrix.IdentityMatrix(x.Height);
            }

            double a = Math.Sign(x[0].R) * x.EuclidianNorm();
            Vector u = x + a * Vector.Ei(x.Size, 0);
            Vector v = u / u.EuclidianNorm();

            ComplexNumber w = (x.ConjugateTranspose() * v).ToComplexNumber() / (v.ConjugateTranspose() * x).ToComplexNumber();
            return (Matrix.IdentityMatrix(x.Height) - (1.0 + w) * v * v.ConjugateTranspose());
        }

        public static Matrix CalculatGivensRotation(ComplexNumber a, ComplexNumber b) {
            ComplexNumber c, s, r;

            if (b == 0) {
                c = 1;
                s = 0;
            } else {
                if (b.Abs() > a.Abs()) {
                    r = a / b;
                    s = 1 / ComplexNumberMath.Sqrt(1 + r * r);
                    c = s * r;
                } else {
                    r = b / a;
                    c = 1 / ComplexNumberMath.Sqrt(1 + r * r);
                    s = c * r;
                }
            }

            Matrix givensRot = new Matrix(2);
            givensRot[0, 0] = c;
            givensRot[0, 1] = s;
            givensRot[1, 0] = -s;
            givensRot[1, 1] = c;

            return givensRot;
        }

    }
}
