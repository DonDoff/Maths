using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class Vector : Matrix {
        public int Size { get; private set; }

        public Vector() : base() { Size = this.Height; }
        public Vector(int size) : base(size, 1) { Size = this.Height; }
        public Vector(Vector v) : base(v) { Size = this.Height; }
        //public Vector(string s) : base(s) { Size = this.Height; }

        public Vector(Matrix mat, int column)
            : base(mat.Height, 1) {
            Size = this.Height;
            if (column >= mat.Width) {
                throw new MatrixException("Matrix does not contain column " + column + "!");
            }
            for (int i = 0; i < Size; i++) {
                this[i] = mat[i, column];
            }
        }

        public static new Vector ParseFrom(string vecString) {
            vecString = vecString.Replace(" ", "");

            string[] rows = vecString.Split(new char[] { ',', ';' });
            Vector newVec = new Vector(rows.Length);

            try {
                for (int i = 0; i < newVec.Size; i++) {
                    newVec[i] = new ComplexNumber(rows[i]);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return newVec;
        }

        public new Vector Copy() {
            return new Vector(this);
        }

        public static Vector Ei(int size, int pos) {
            Vector v = Vector.Zeros(size);
            v[pos] = 1;
            return v;
        }

        public double EuclidianNorm() {
            ComplexNumber norm = 0;
            for (int i = 0; i < Size; i++) {
                norm += this[i] * this[i];
            }
            return Math.Sqrt(norm.R);
        }

        public Vector CrossProduct(Vector v) {
            if (Size != 3) {
                throw new MatrixException("Cross product only exists in 3D");
            }
            Vector newV = new Vector(3);
            newV[0] = this[2] * v[3] - v[2] * this[3];
            newV[1] = this[3] * v[1] - v[3] * this[1];
            newV[2] = this[1] * v[2] - v[1] * this[2];
            return newV;
        }

        public ComplexNumber DotProduct(Vector v) {
            if (Size != v.Size) {
                throw new IncompatibleMatrixDimensionsException(this, v);
            }
            ComplexNumber cSum = new ComplexNumber();
            for (int i = 0; i < Size; i++) {
                cSum += this[i] * v[i].Conjugate();
            }
            return cSum;
        }

        public Vector CircShift(int shift) {
            Vector newV = new Vector(Size);
            for (int i = 0; i < Size; i++) {
                newV[i] = this[ComplexNumberMath.Mod((i + shift), Size)];
            }
            return newV;
        }

        // Fill the vector with 1s
        public static Vector Zeros(int size) {
            return Matrix.Zeros(size, 1).ToColumnVector();
        }

        // Fill the vector with 1s
        public static Vector Ones(int size) {
            return Matrix.Ones(size, 1).ToColumnVector();
        }

        // Fill the vector with random complex numbers
        public static Vector RandomComplex(int size, Random seed) {
            return Matrix.RandomComplex(size, 1, seed).ToColumnVector();
        }

        // Fill the vector with random real numbers
        public static Vector RandomReal(int size, Random seed) {
            return Matrix.RandomReal(size, 1, seed).ToColumnVector();
        }

        // Create a vector starting from 0 until end, in steps of 1
        public static Vector Linspace(double end) {
            return Linspace(0, end);
        }

        // Create a vector starting from start until end, in steps of 1
        public static Vector Linspace(double start, double end) {
            return Linspace(start, end, (int)(end - start) - 1);
        }

        // Create a vector starting from start until end, in the specified amount of steps
        public static Vector Linspace(double start, double end, int steps) {
            Vector v = new Vector(0);
            double length = end - start;
            if (length >= 0) {
                double delta = (length + 0.0) / steps;
                v = new Vector(steps);
                for (int i = 0; i < steps; i++) {
                    v[i] = start + i * delta;
                }
            }
            return v;
        }

        // Create a vector starting from 0 until end, in steps of 1
        public static Vector Arrange(double end) {
            return Arrange(0, end);
        }

        // Create a vector starting from start until end, in steps of 1
        public static Vector Arrange(double start, double end) {
            return Arrange(start, end, 1);
        }

        // Create a vector starting from start until end, in steps of delta
        public static Vector Arrange(double start, double end, double delta) {
            Vector v = new Vector(0);
            double length = end - start;
            if (length >= 0) {
                int steps = (int)Math.Round(length / delta);
                v = new Vector(steps);
                for (int i = 0; i < steps; i++) {
                    v[i] = start + i * delta;
                }
            }
            return v;
        }

        // Computes the sum of the vector elements
        public ComplexNumber Sum() {
            ComplexNumber sum = 0;
            for (int i = 0; i < Size; i++) {
                sum += this[i];
            }
            return sum;
        }

        public bool IsOrthogonalTo(Vector v) {
            return DotProduct(v) == 0;
        }

        // Overloads the index operator.
        public ComplexNumber this[int pos] {
            get {
                return this[pos, 0];
            }
            set {
                this[pos, 0] = value;
            }
        }

        public Vector this[Vector v] {
            get {
                return this[v, 0];
            }
            set {
                this[v, 0] = value;
            }
        }


        public static Vector operator +(ComplexNumber scalar, Vector vec1) {
            return (vec1.Add(scalar)).ToColumnVector();
        }

        public static Vector operator +(Vector vec1, ComplexNumber scalar) {
            return scalar + vec1;
        }

        public static Vector operator -(ComplexNumber scalar, Vector vec1) {
            return scalar + (-1 * vec1);
        }

        public static Vector operator -(Vector vec1, ComplexNumber scalar) {
            return vec1 + (-1 * scalar);
        }

        public static Vector operator *(ComplexNumber scalar, Vector vec1) {
            return (vec1.Multiply(scalar)).ToColumnVector();
        }

        public static Vector operator *(Vector vec1, ComplexNumber scalar) {
            return scalar * vec1;
        }

        public static Vector operator /(Vector vec1, ComplexNumber scalar) {
            return (vec1.Divide(scalar)).ToColumnVector();
        }

        public static Vector operator +(Vector vec1, Vector vec2) {
            return (vec1.Add(vec2)).ToColumnVector();
        }

        public static Vector operator -(Vector vec1, Vector vec2) {
            return (vec1.Subtract(vec2)).ToColumnVector();
        }

        public static Vector operator -(Vector vec1) {
            return -1*vec1.ToColumnVector();
        }

    }
}
