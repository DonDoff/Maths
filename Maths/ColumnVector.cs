using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public class ColumnVector : Matrix {
        public int Size { get; private set; }

        public ColumnVector() : base() { Size = this.Height; }
        public ColumnVector(int size) : base(size, 1) { Size = this.Height; }
        public ColumnVector(ColumnVector v) : base(v) { Size = this.Height; }

        public ColumnVector(Matrix mat, int column)
            : base(mat.Height, 1) {
            Size = this.Height;
            if (column >= mat.Width) {
                throw new MatrixException("Matrix does not contain column " + column + "!");
            }
            for (int i = 0; i < Size; i++) {
                this[i] = mat[i, column];
            }
        }

        public static new ColumnVector ParseFrom(string vecString) {
            vecString = vecString.Replace(" ", "");

            string[] rows = vecString.Split(new char[] { ',', ';' });
            ColumnVector newVec = new ColumnVector(rows.Length);

            try {
                for (int i = 0; i < newVec.Size; i++) {
                    newVec[i] = new ComplexNumber(rows[i]);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return newVec;
        }

        public new ColumnVector Copy() {
            return new ColumnVector(this);
        }

        public static ColumnVector Ei(int size, int pos) {
            ColumnVector v = ColumnVector.Zeros(size);
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

        public ColumnVector CrossProduct(ColumnVector v) {
            if (Size != 3) {
                throw new MatrixException("Cross product only exists in 3D");
            }
            ColumnVector newV = new ColumnVector(3);
            newV[0] = this[2] * v[3] - v[2] * this[3];
            newV[1] = this[3] * v[1] - v[3] * this[1];
            newV[2] = this[1] * v[2] - v[1] * this[2];
            return newV;
        }

        public ComplexNumber DotProduct(ColumnVector v) {
            if (Size != v.Size) {
                throw new IncompatibleMatrixDimensionsException(this, v);
            }
            ComplexNumber cSum = new ComplexNumber();
            for (int i = 0; i < Size; i++) {
                cSum += this[i] * v[i].Conjugate();
            }
            return cSum;
        }

        public ColumnVector CircShift(int shift) {
            ColumnVector newV = new ColumnVector(Size);
            for (int i = 0; i < Size; i++) {
                newV[i] = this[ComplexNumberMath.Mod((i + shift), Size)];
            }
            return newV;
        }

        // Fill the vector with 1s
        public static ColumnVector Zeros(int size) {
            return Matrix.Zeros(size, 1).ToColumnVector();
        }

        // Fill the vector with 1s
        public static ColumnVector Ones(int size) {
            return Matrix.Ones(size, 1).ToColumnVector();
        }

        // Fill the vector with random complex numbers
        public static ColumnVector RandomComplex(int size, Random seed) {
            return Matrix.RandomComplex(size, 1, seed).ToColumnVector();
        }

        // Fill the vector with random real numbers
        public static ColumnVector RandomReal(int size, Random seed) {
            return Matrix.RandomReal(size, 1, seed).ToColumnVector();
        }

        // Create a vector starting from 0 until end, in steps of 1
        public static ColumnVector Linspace(double end) {
            return Linspace(0, end);
        }

        // Create a vector starting from start until end, in steps of 1
        public static ColumnVector Linspace(double start, double end) {
            return Linspace(start, end, (int)(end - start) - 1);
        }

        // Create a vector starting from start until end, in the specified amount of steps
        public static ColumnVector Linspace(double start, double end, int steps) {
            ColumnVector v = new ColumnVector(0);
            double length = end - start;
            if (length >= 0) {
                double delta = (length + 0.0) / steps;
                v = new ColumnVector(steps);
                for (int i = 0; i < steps; i++) {
                    v[i] = start + i * delta;
                }
            }
            return v;
        }

        // Create a vector starting from 0 until end, in steps of 1
        public static ColumnVector Arrange(double end) {
            return Arrange(0, end);
        }

        // Create a vector starting from start until end, in steps of 1
        public static ColumnVector Arrange(double start, double end) {
            return Arrange(start, end, 1);
        }

        // Create a vector starting from start until end, in steps of delta
        public static ColumnVector Arrange(double start, double end, double delta) {
            ColumnVector v = new ColumnVector(0);
            double length = end - start;
            if (length >= 0) {
                int steps = (int)Math.Round(length / delta);
                v = new ColumnVector(steps);
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

        public bool IsOrthogonalTo(ColumnVector v) {
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

        public ColumnVector this[ColumnVector v] {
            get {
                return this[v, 0];
            }
            set {
                this[v, 0] = value;
            }
        }


        public static ColumnVector operator +(ComplexNumber scalar, ColumnVector vec1) {
            return (vec1.Add(scalar)).ToColumnVector();
        }

        public static ColumnVector operator +(ColumnVector vec1, ComplexNumber scalar) {
            return scalar + vec1;
        }

        public static ColumnVector operator -(ComplexNumber scalar, ColumnVector vec1) {
            return scalar + (-1 * vec1);
        }

        public static ColumnVector operator -(ColumnVector vec1, ComplexNumber scalar) {
            return vec1 + (-1 * scalar);
        }

        public static ColumnVector operator *(ComplexNumber scalar, ColumnVector vec1) {
            return (vec1.Multiply(scalar)).ToColumnVector();
        }

        public static ColumnVector operator *(ColumnVector vec1, ComplexNumber scalar) {
            return scalar * vec1;
        }

        public static ColumnVector operator /(ColumnVector vec1, ComplexNumber scalar) {
            return (vec1.Divide(scalar)).ToColumnVector();
        }

        public static ColumnVector operator +(ColumnVector vec1, ColumnVector vec2) {
            return (vec1.Add(vec2)).ToColumnVector();
        }

        public static ColumnVector operator -(ColumnVector vec1, ColumnVector vec2) {
            return (vec1.Subtract(vec2)).ToColumnVector();
        }

        public static ColumnVector operator -(ColumnVector vec1) {
            return -1*vec1.ToColumnVector();
        }

    }
}
