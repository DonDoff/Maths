using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class ComplexNumberMath {
        public static ComplexNumber Sin(ComplexNumber c) {
            return Math.Sin(c.R) * Math.Cosh(c.I) +
                ComplexNumber.UNIT_I * Math.Cos(c.R) * Math.Sinh(c.I);
        }

        public static ComplexNumber Cos(ComplexNumber c) {
            return Math.Cos(c.R) * Math.Cosh(c.I) -
                ComplexNumber.UNIT_I * Math.Sin(c.R) * Math.Sinh(c.I);
        }

        public static ComplexNumber Sinh(ComplexNumber c) {
            return Math.Sinh(c.R) * Math.Cos(c.I) +
                ComplexNumber.UNIT_I * Math.Cosh(c.R) * Math.Sin(c.I);
        }

        public static ComplexNumber Cosh(ComplexNumber c) {
            return Math.Cosh(c.R) * Math.Cos(c.I) -
                ComplexNumber.UNIT_I * Math.Sinh(c.R) * Math.Sin(c.I);
        }

        public static ComplexNumber Sinc(ComplexNumber c) {
            if (c == 0) {
                return 1;
            }
            return Sin(Math.PI * c) / (Math.PI * c);
        }

        public static ComplexNumber Min(ComplexNumber c1, ComplexNumber c2) {
            return c1 < c2 ? c1 : c2;
        }

        public static ComplexNumber Max(ComplexNumber c1, ComplexNumber c2) {
            return c1 > c2 ? c1 : c2;
        }

        public static int Mod(int a, int b) {
            return (Math.Abs(a * b) + a) % b;
        }

        public static ComplexNumber Exp(ComplexNumber c) {
            double r = Math.Exp(c.R);
            return new ComplexNumber(r * Math.Cos(c.I), r * Math.Sin(c.I));
        }

        public static double Abs(ComplexNumber c) {
            return Math.Sqrt(c.R * c.R + c.I * c.I);
        }

        public static double Argument(ComplexNumber c) {
            return Math.Atan2(c.I, c.R);
        }
        
        public static ComplexNumber Pow(ComplexNumber c, double n) {
            // De Moivre's theorem
            double r = Abs(c);
            double arg = Argument(c);
            
            return Math.Pow(r, n)*(Math.Cos(n*arg) + ComplexNumber.UNIT_I*Math.Sin(n*arg));
        }

        public static ComplexNumber Sqrt(ComplexNumber c) {
            return Pow(c, 1.0/2);
        }

    }
}
