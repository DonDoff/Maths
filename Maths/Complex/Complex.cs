using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Maths {
    public class Complex {
        private const int roundedDecimals = 3;

        public static Complex MAX = new Complex(double.MaxValue, double.MaxValue);
        public static Complex MIN = new Complex(double.MinValue, double.MinValue);
        public static Complex UNIT_I = new Complex(0, 1);

        public double R { get; set; }
        public double I { get; set; }

        public Complex() {
            R = 0;
            I = 0;
        }

        public Complex(double r, double i) {
            R = r;
            I = i;
        }

        public Complex(Complex c) {
            R = c.R;
            I = c.I;
        }

        public Complex(string cString) {
            cString = cString.Replace(" ", "");
            Regex regex = new Regex(@"^(?:\+?(?<r>-?(?:\d+\.?\d*|\d*\.?\d+))" + //Find a complex number and name the first group r. Capture the - and not the + sign.
                @"(?=[+-])\+?(?<i>-?(?:\d+\.?\d*|\d*\.?\d+)?[ij]))" +           //Find the imaginary number corresponding with the complex number and name the group i. Capture the - and not the + sign.
                @"|(?:\+?(?<i>-?(?:\d+\.?\d*|\d*\.?\d+)?[ij]))" +               //OR find a pure imaginary number (without a real part) and name the group i. Capture the - and not the + sign.
                @"|\+?(?<r>-?(?:\d+\.?\d*|\d*\.?\d+))$");                       //OR find a pure real number (without an imaginary part) and name the group r. Capture the - and not the + sign.
            Match match = regex.Match(cString);
            string Rs = match.Groups["r"].Value;
            string Is = match.Groups["i"].Value;

            if (Rs.Length > 0) {
                R = Double.Parse(Rs, CultureInfo.InvariantCulture);
            }
            if (Is.Length > 0) {
                // Remove i
                Is = Is.Substring(0, Is.Length - 1);

                if (Is.Length > 0) {    
                    if (Is.Length == 1 && Is[0] == '-') {   // I is -i
                        I = -1;
                    } else {
                        I = Double.Parse(Is, CultureInfo.InvariantCulture);
                    }
                } else if (Is.Length == 0) { // I is i
                    I = 1;
                }
            }
        }

        public static implicit operator Complex(double scalar) {
            return new Complex(scalar, 0);
        }

        public Complex Conjugate() {
            return new Complex(R, -I);
        }

        public Complex Add(Complex c) {
            return new Complex(R + c.R, I + c.I);
        }

        public Complex Multiply(Complex c) {
            return new Complex(R * c.R - I * c.I, R*c.I + I*c.R);
        }
        
        public Complex Divide(Complex c) {
            return new Complex((R * c.R + I * c.I) / (c.R * c.R + c.I * c.I), 
                (I * c.R - R * c.I) / (c.R * c.R + c.I * c.I));
        }

        public Complex Round(int decimals) {
            return new Complex(Math.Round(R, decimals), Math.Round(I, decimals));
        }

        public bool IsReal() {
            return I != 0;
        }

        public bool IsPureImaginary() {
            return R != 0;
        }

        // Addition operators
        public static Complex operator +(Complex c1, Complex c2) {
            return c1.Add(c2);
        }

        // Subtract operators
        public static Complex operator -(Complex c1) {
            return -1*c1;
        }

        // Subtract operators
        public static Complex operator -(Complex c1, Complex c2) {
            return c1.Add(-1 * c2);
        }

        // Multiply operators        
        public static Complex operator *(Complex c1, Complex c2) {
            return c1.Multiply(c2);
        }

        // Division operators
        public static Complex operator /(Complex c1, Complex c2) {
            return c1.Divide(c2);
        }

        public static bool operator >(Complex c1, Complex c2) {
            return c1.R > c2.R;
        }

        public static bool operator >=(Complex c1, Complex c2) {
            return c1.R >= c2.R;
        }

        public static bool operator <(Complex c1, Complex c2) {
            return c1.R < c2.R;
        }

        public static bool operator <=(Complex c1, Complex c2) {
            return c1.R<= c2.R;
        }

        private bool fieldsMatch(Complex c) {
            return Math.Abs(R - c.R) < Constants.EPS && Math.Abs(I - c.I) < Constants.EPS;
        }

        public override bool Equals(System.Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to Complex return false.
            Complex c = obj as Complex;
            if ((System.Object)c == null) {
                return false;
            }

            // Return true if the fields match:
            return fieldsMatch(c);
        }

        public bool Equals(Complex c) {
            // If parameter is null return false:
            if ((object)c == null) {
                return false;
            }

            // Return true if the fields match:
            return fieldsMatch(c);
        }

        public override int GetHashCode() {
            return (int) R ^ (int) I;
        }
        
        public static bool operator ==(Complex c1, Complex c2) {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(c1, c2)) {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)c1 == null) || ((object)c2 == null)) {
                return false;
            }

            // Return true if the fields match:
            return c1.fieldsMatch(c2);
        }

        public static bool operator !=(Complex c1, Complex c2) {
            return !(c1 == c2);
        }
        
        public override string ToString() {
            double Rr = Math.Round(R, roundedDecimals);
            double Ir = Math.Round(I, roundedDecimals);

            string sign = "";
            sign = Ir > 0 ? "+" : "-";
            Ir = Math.Abs(Ir);

            string Rs = Rr.ToString(CultureInfo.InvariantCulture);
            string Is = Ir.ToString(CultureInfo.InvariantCulture);

            if (Ir == 0) {
                return Rs;
            }
            if (Rr == 0) {
                return Ir == 1 ? sign == "+" ? "i" : "-i" : Is + "i";
            }

            return Ir == 1 ? Rs + sign + "i" : Rs + sign + Is + "i";
        }
    }

    class ComplexException : Exception {
        public ComplexException(string message) : base(message) { }
    }
}
