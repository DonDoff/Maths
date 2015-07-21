using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public static class ComplexMath {
        public static Complex Sin(Complex c) {
            return Math.Sin(c.R) * Math.Cosh(c.I) +
                Complex.UNIT_I * Math.Cos(c.R) * Math.Sinh(c.I);
        }

        public static Complex Cos(Complex c) {
            return Math.Cos(c.R) * Math.Cosh(c.I) -
                Complex.UNIT_I * Math.Sin(c.R) * Math.Sinh(c.I);
        }

        public static Complex Sinh(Complex c) {
            return Math.Sinh(c.R) * Math.Cos(c.I) +
                Complex.UNIT_I * Math.Cosh(c.R) * Math.Sin(c.I);
        }

        public static Complex Cosh(Complex c) {
            return Math.Cosh(c.R) * Math.Cos(c.I) -
                Complex.UNIT_I * Math.Sinh(c.R) * Math.Sin(c.I);
        }

        public static Complex Sinc(Complex c) {
            if (c == 0) {
                return 1;
            }
            return Sin(Math.PI * c) / (Math.PI * c);
        }

        public static Complex Min(Complex c1, Complex c2) {
            return c1 < c2 ? c1 : c2;
        }

        public static Complex Max(Complex c1, Complex c2) {
            return c1 > c2 ? c1 : c2;
        }

        public static int Mod(int a, int b) {
            return (Math.Abs(a * b) + a) % b;
        }

        public static Complex Exp(Complex c) {
            double r = Math.Exp(c.R);
            return new Complex(r * Math.Cos(c.I), r * Math.Sin(c.I));
        }

        public static double Abs(Complex c) {
            return Math.Sqrt(c.R * c.R + c.I * c.I);
        }

        public static double Argument(Complex c) {
            return Math.Atan2(c.I, c.R);
        }

        public static Complex Pow(this Complex c, double n) {
            // De Moivre's theorem
            double r = Abs(c);
            double arg = Argument(c);
            
            return Math.Pow(r, n)*(Math.Cos(n*arg) + Complex.UNIT_I*Math.Sin(n*arg));
        }

        public static Complex Sqrt(this Complex c) {
            return Pow(c, 1.0/2);
        }

        /// <summary>
        /// Returns a new complex number with the real and imaginary part between -1 and 1.
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Complex Rand(Random seed = null) {
            if (seed == null) {
                seed = new Random();
            }
            return new Complex(seed.NextDouble() * 2 - 1, seed.NextDouble() * 2 - 1);
        }
    }
}
