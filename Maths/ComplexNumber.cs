using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Maths {
    public class ComplexNumber {

        private const double EPSILON = 1e-10;
        private const int roundedDecimals = 2;

        public static ComplexNumber MAX = new ComplexNumber(double.MaxValue, double.MaxValue);
        public static ComplexNumber MIN = new ComplexNumber(double.MinValue, double.MinValue);
        public static ComplexNumber UNIT_I = new ComplexNumber(0, 1);

        public double R { get; set; }
        public double I { get; set; }

        public ComplexNumber() {
            R = 0;
            I = 0;
        }

        public ComplexNumber(double r, double i) {
            R = r;
            I = i;
        }

        public ComplexNumber(ComplexNumber c) {
            R = c.R;
            I = c.I;
        }

        public ComplexNumber(string cString) {
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

        public static implicit operator ComplexNumber(double scalar) {
            return new ComplexNumber(scalar, 0);
        }

        public double Abs() {
            return ComplexNumberMath.Abs(this);
        }

        public ComplexNumber Conjugate() {
            return new ComplexNumber(R, -I);
        }

        public ComplexNumber Add(ComplexNumber c) {
            return new ComplexNumber(R + c.R, I + c.I);
        }

        public ComplexNumber Multiply(ComplexNumber c) {
            return new ComplexNumber(R * c.R - I * c.I, R*c.I + I*c.R);
        }
        
        public ComplexNumber Divide(ComplexNumber c) {
            return new ComplexNumber((R * c.R + I * c.I) / (c.R * c.R + c.I * c.I), 
                (I * c.R - R * c.I) / (c.R * c.R + c.I * c.I));
        }

        public ComplexNumber Round(int decimals) {
            return new ComplexNumber(Math.Round(R, decimals), Math.Round(I, decimals));
        }

        public bool IsReal() {
            return I != 0;
        }

        public bool IsPureImaginary() {
            return R != 0;
        }

        // Addition operators
        public static ComplexNumber operator +(ComplexNumber c1, ComplexNumber c2) {
            return c1.Add(c2);
        }

        // Subtract operators
        public static ComplexNumber operator -(ComplexNumber c1) {
            return -1*c1;
        }

        // Subtract operators
        public static ComplexNumber operator -(ComplexNumber c1, ComplexNumber c2) {
            return c1.Add(-1 * c2);
        }

        // Multiply operators        
        public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2) {
            return c1.Multiply(c2);
        }

        // Division operators
        public static ComplexNumber operator /(ComplexNumber c1, ComplexNumber c2) {
            return c1.Divide(c2);
        }

        public static bool operator >(ComplexNumber c1, ComplexNumber c2) {
            return c1.R > c2.R;
        }

        public static bool operator >=(ComplexNumber c1, ComplexNumber c2) {
            return c1.R >= c2.R;
        }

        public static bool operator <(ComplexNumber c1, ComplexNumber c2) {
            return c1.R < c2.R;
        }

        public static bool operator <=(ComplexNumber c1, ComplexNumber c2) {
            return c1.R<= c2.R;
        }

        private bool fieldsMatch(ComplexNumber c) {
            return Math.Abs(R - c.R) < EPSILON && Math.Abs(I - c.I) < EPSILON;
        }

        public override bool Equals(System.Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to Complex return false.
            ComplexNumber c = obj as ComplexNumber;
            if ((System.Object)c == null) {
                return false;
            }

            // Return true if the fields match:
            return fieldsMatch(c);
        }

        public bool Equals(ComplexNumber c) {
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
        
        public static bool operator ==(ComplexNumber c1, ComplexNumber c2) {
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

        public static bool operator !=(ComplexNumber c1, ComplexNumber c2) {
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
