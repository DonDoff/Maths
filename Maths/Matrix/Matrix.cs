using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Maths {
    public class Matrix {

        //////////////////////////////////////////////////////
        //                   Matrix fields                  //
        //////////////////////////////////////////////////////

        public int Height { get; private set; }
        public int Width { get; private set; }
        private ComplexNumber[,] Mat { get; set; }

        //////////////////////////////////////////////////////
        //               Matrix Constructors                //
        //////////////////////////////////////////////////////

        // Constructs an empty 0x0 matrix.
        public Matrix() : this(0) { }

        // Constructs a square NxN matrix.
        public Matrix(int N) : this(N, N) { }

        // Constructs a matrix with a height and width, filled with zeros.
        public Matrix(int height, int width) {
            Height = height;
            Width = width;
            Mat = new ComplexNumber[Height, Width];
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    Mat[i, j] = 0;
                }
            }
        }

        // Constructs a Matrix from another matrix, copying the elements.
        public Matrix(Matrix m) {
            Height = m.Height;
            Width = m.Width;
            Mat = new ComplexNumber[Height, Width];
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    Mat[i, j] = m[i, j];
                }
            }
        }

        

        //////////////////////////////////////////////////////
        //               Index Operations                   //
        //////////////////////////////////////////////////////

        public ComplexNumber this[int row, int column] {
            get {
                return Mat[row, column];
            }
            set {
                Mat[row, column] = value;
            }
        }

        public Vector this[Vector v, int column] {
            get {
                Vector newVec = new Vector(v.Size);
                for (int i = 0; i < v.Size; i++) {
                    newVec[i] = Mat[(int)v[i].R, column];
                }
                return newVec;
            }
            set {
                for (int i = 0; i < value.Size; i++) {
                    Mat[(int)v[i].R, column] = value[i];
                }
            }
        }

        public Vector this[int row, Vector v] {
            get {
                Vector newVec = new Vector(v.Size);
                for (int i = 0; i < v.Size; i++) {
                    newVec[i] = Mat[row, (int)v[i].R];
                }
                return newVec;
            }
            set {
                for (int i = 0; i < value.Size; i++) {
                    Mat[row, (int)v[i].R] = value[i];
                }
            }
        }

        public Matrix this[Vector column, Vector row] {
            get {
                Matrix newMat = new Matrix(column.Size, row.Size);
                for (int i = 0; i < column.Size; i++) {
                    for (int j = 0; j < row.Size; j++) {
                        newMat[i, j] = Mat[(int)column[i].R, (int)row[j].R];
                    }
                }
                return newMat;
            }
            set {
                for (int i = 0; i < value.Height; i++) {
                    for (int j = 0; j < value.Width; j++) {
                        Mat[(int)column[i].R, (int)row[j].R] = value[i, j];
                    }
                }
            }
        }

        //////////////////////////////////////////////////////
        //               Matrix Operations                  //
        //////////////////////////////////////////////////////

        /// <summary>
        /// Check if the dimensions and the elements of the matrices are equal.
        /// </summary>
        /// <param name="mat2"></param>
        /// <returns>true if all values in A == corresponding values in B</returns>
        public bool MatrixEquals(Matrix mat2) {
            if (!AreDimensionsEqual(mat2)) {
                return false;
            }

            for (int i = 0; i < Height; ++i) {
                for (int j = 0; j < Width; ++j) {
                    if (this[i, j] != mat2[i, j]) {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool Equals(System.Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to Matrix return false:
            Matrix mat2 = obj as Matrix;
            if ((object)mat2 == null) {
                return false;
            }

            // Return true if the fields match:
            return MatrixEquals(mat2);
        }

        public bool Equals(Matrix mat2) {
            // If parameter is null return false:
            if ((object)mat2 == null) {
                return false;
            }

            // Return true if the fields match:
            return MatrixEquals(mat2);
        }

        public override int GetHashCode() {
            return base.GetHashCode() ^ (Width + Height);
        }

        // Adds a scalar to the matrix and returns the result.
        public Matrix Add(ComplexNumber scalar) {
            return AppyToAllElements(x => x + scalar);
        }

        // Adds two matrices and returns the result.
        public Matrix Add(Matrix mat2) {
            if (Height != mat2.Height || Width != mat2.Width) {
                throw new IncompatibleMatrixDimensionsException(this, mat2);
            }

            Matrix newMat = new Matrix(Height, Width);
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    newMat[i, j] = this[i, j] + mat2[i, j];
                }
            }
            return newMat;
        }

        // Subtracts two matrices and returns the result.
        public Matrix Subtract(Matrix mat2) {
            return this.Add(new ComplexNumber(-1, 0) * mat2);
        }

        // Multiplies a scalar with a matrix and returns the result.
        public Matrix Multiply(ComplexNumber scalar) {
            return AppyToAllElements(x => x * scalar);
        }

        public Matrix Divide(ComplexNumber scalar) {
            return Multiply(1.0 / scalar);
        }

        // Multiplies two matrices and returns the result.
        public Matrix Multiply(Matrix mat2) {
            if (Width != mat2.Height) {
                throw new IncompatibleMatrixDimensionsException(this, mat2);
            }

            ComplexNumber sum = 0;
            Matrix newMat = new Matrix(Height, mat2.Width);
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < mat2.Width; j++) {
                    for (int k = 0; k < Width; k++) {
                        sum += this[i, k] * mat2[k, j];
                    }
                    newMat[i, j] = sum;
                    sum = 0;
                }
            }

            return newMat;
        }

        // Multiplies two matrices element-wise and returns the result.
        public Matrix ElementMultiply(Matrix mat2) {
            if (Width != mat2.Width || Height != mat2.Height) {
                throw new IncompatibleMatrixDimensionsException(this, mat2);
            }

            Matrix newMat = new Matrix(Height, Width);
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    newMat[i, j] = this[i, j] * mat2[i, j];
                }
            }

            return newMat;
        }

        // Computes the trace of the matrix.
        public ComplexNumber Trace() {
            if (!IsSquare()) {
                throw new MatrixException("Matrix needs to be square!");
            }

            ComplexNumber trace = 0;
            for (int i = 0; i < Height; i++) {
                trace += this[i, i];
            }

            return trace;
        }

        // Transpose matrix
        public Matrix Transpose() {
            Matrix newMat = new Matrix(Width, Height);
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    newMat[j, i] = this[i, j];
                }
            }
            return newMat;
        }

        public Matrix ConjugateTranspose() {
            Matrix newMat = new Matrix(Width, Height);
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    newMat[j, i] = this[i, j].Conjugate();
                }
            }
            return newMat;
        }

        public ComplexNumber Determinant() {
            LUDecomposition lu = LU();
            ComplexNumber det = lu.DetOfP;
            for (int i = 0; i < Height; i++) {
                det *= lu.U[i, i];
            }
            return det;
        }

        /// Function returns the inverted matrix
        public Matrix Invert() {
            SolveLinearEquations sle = new SolveLinearEquations(this);
            return sle.Solve(MatrixFactory.IdentityMatrix(Height));
        }

        public LUDecomposition LU() {
            return new LUDecomposition(this);
        }

        public CholeskyDecomposition Chol() {
            return new CholeskyDecomposition(this);
        }

        public QRDecomposition QR() {
            return new QRDecomposition(this);
        }

        public EigenvalueDecomposition Eigen() {
            return new EigenvalueDecomposition(this);
        }

        public SVD SVD() {
            return new SVD(this);
        }

        public Bidiagonalization Bidiagonalization() {
            return new Bidiagonalization(this);
        }

        public HessenbergDecomposition Hessen() {
            return new HessenbergDecomposition(this);
        }

        public Matrix Copy() {
            return new Matrix(this);
        }

        public Vector GetColumnVector(int column) {
            if (column < 0 || column >= Width) {
                throw new MatrixException("Exceeds matrix dimensions!");
            }

            Vector v = new Vector(Height);
            for (int i = 0; i < Height; i++) {
                v[i] = this[i, column];
            }
            return v;
        }

        public void SetColumnVector(Vector v, int column) {
            if (column < 0 || column >= Width || Height != v.Size) {
                throw new MatrixException("Exceeds matrix dimensions!");
            }

            for (int i = 0; i < Height; i++) {
                this[i, column] = v[i];
            }
        }

        public Matrix SwapRows(int row1, int row2) {
            if (row1 >= Height || row2 >= Height || row1 < 0 || row2 < 0) {
                throw new MatrixException("Outside the bounds of the matrix!");
            }
            Matrix newMat = this.Copy();
            for (int i = 0; i < Width; i++) {
                ComplexNumber tempNumber = this[row1, i];
                newMat[row1, i] = this[row2, i];
                newMat[row2, i] = tempNumber;
            }
            return newMat;
        }

        public bool Contains(ComplexNumber c) {
            foreach (ComplexNumber cElement in Mat) {
                if (cElement == c) {
                    return true;
                }
            }
            return false;
        }

        public Matrix ConcatenateRows(Matrix m2) {
            if (Width != m2.Width) {
                throw new IncompatibleMatrixDimensionsException(this, m2);
            }
            Matrix newMat = new Matrix(Height + m2.Height, Width);
            for (int j = 0; j < Width; j++) {
                for (int i = 0; i < Height; i++) {
                    newMat[i, j] = this[i, j];
                }
                for (int i = Height; i < Height + m2.Height; i++) {
                    newMat[i, j] = m2[i - Height, j];
                }
            }
            return newMat;
        }

        public Matrix ConcatenateColumns(Matrix m2) {
            if (Height != m2.Height) {
                throw new IncompatibleMatrixDimensionsException(this, m2);
            }
            Matrix newMat = new Matrix(Height, Width + m2.Width);
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    newMat[i, j] = this[i, j];
                }
                for (int j = Width; j < Width + m2.Width; j++) {
                    newMat[i, j] = m2[i, j - Width];
                }
            }
            return newMat;
        }

        //////////////////////////////////////////////////////
        //                 Matrix Checks                    //
        //////////////////////////////////////////////////////
        public bool AreDimensionsEqual(Matrix mat2) {
            return Height == mat2.Height && Width == mat2.Width;
        }

        public bool IsSquare() {
            return Height == Width;
        }

        public bool IsReal() {
            return CheckAllElements(x => x.IsReal());
        }

        public bool IsPureImaginary() {
            return CheckAllElements(x => x.IsPureImaginary());
        }

        public bool IsSymmetric() {
            return IsSquare() && this == this.Transpose();
        }

        public bool IsHermitian() {
            return IsSquare() && this == this.ConjugateTranspose();
        }

        public bool IsDiagonal() {
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    if (i != j && this[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsBiDiagonal() {
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    if (!(i == j || j == i + 1)  && this[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsUpperTriangular() {
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < i; j++) {
                    if (this[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsLowerTriangular() {
            return this.Transpose().IsUpperTriangular();
        }

        public bool IsUpperHessenberg() {
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < i - 1; j++) {
                    if (this[i, j] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsUnitary() {
            return IsSquare() && (this.ConjugateTranspose() * this) == MatrixFactory.IdentityMatrix(this.Height);
        }

        public bool IsPositiveDefinite() {
            try {
                CholeskyDecomposition ch = Chol();
                return ch.IsPositiveDefinite;
            } catch (MatrixException) {
                return false;
            }
        }

        public bool IsSingular() {
            return Determinant() == 0;
        }

        //////////////////////////////////////////////////////
        //                Matrix Conversions                //
        //////////////////////////////////////////////////////

        public Matrix RealPart() {
            return AppyToAllElements(x => x.R);
        }

        public Matrix ImPart() {
            return AppyToAllElements(x => new ComplexNumber(0, x.I));
        }

        public Vector ToColumnVector() {
            if (Width != 1) {
                throw new MatrixException("Wrong matrix dimension to convert to column vector!");
            }
            Vector v = new Vector(Height);
            for (int i = 0; i < Height; i++) {
                v[i] = Mat[i, 0];
            }
            return v;
        }

        public ComplexNumber ToComplexNumber() {
            if (Height != 1 || Width != 1) {
                throw new MatrixException("Wrong matrix dimension to convert to ComplexNumber!");
            }
            return Mat[0, 0];
        }

        public Matrix SubMatrix(int rowStart, int rowEnd, int columnStart, int columnEnd) {
            int newHeight = rowEnd - rowStart;
            int newWidth = columnEnd - columnStart;

            if (newHeight < 0 || newHeight > Height || newWidth < 0 || newWidth > Width) {
                throw new MatrixException("Can not make a submatrix with the given dimensions!");
            }
            Matrix subMat = new Matrix(newHeight, newWidth);
            for (int i = 0; i < newHeight; i++) {
                for (int j = 0; j < newWidth; j++) {
                    subMat[i, j] = this[i + rowStart, j + columnStart];
                }
            }
            return subMat;
        }

        //////////////////////////////////////////////////////
        //           Matrix Delegate Functions              //
        //////////////////////////////////////////////////////

        /// <summary>
        /// Apply the func on all matrix elements and returns a new matrix
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Matrix AppyToAllElements(Func<ComplexNumber, ComplexNumber> func) {
            Matrix newMat = new Matrix(Height, Width);
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    newMat[i, j] = func(this[i, j]);
                }
            }
            return newMat;
        }

        /// <summary>
        /// Check the func on all elements of the matrix
        /// </summary>
        /// <param name="func"></param>
        /// <returns>false if the action is true</returns>
        public bool CheckAllElements(Func<ComplexNumber, bool> func) {
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    if (func(this[i, j])) {
                        return false;
                    }
                }
            }
            return true;
        }

        //////////////////////////////////////////////////////
        //              Overloaded operators                //
        //////////////////////////////////////////////////////

        public static bool operator ==(Matrix mat1, Matrix mat2) {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(mat1, mat2)) {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)mat1 == null) || ((object)mat2 == null)) {
                return false;
            }

            // Return true if the fields match:
            return mat1.MatrixEquals(mat2);
        }

        public static bool operator !=(Matrix mat1, Matrix mat2) {
            return !(mat1 == mat2);
        }

        public static Matrix operator +(ComplexNumber scalar, Matrix mat1) {
            return mat1.Add(scalar);
        }

        public static Matrix operator +(Matrix mat1, ComplexNumber scalar) {
            return scalar + mat1;
        }

        public static Matrix operator -(ComplexNumber scalar, Matrix mat1) {
            return scalar + (-1 * mat1);
        }

        public static Matrix operator -(Matrix mat1, ComplexNumber scalar) {
            return mat1 + (-1 * scalar);
        }

        public static Matrix operator *(ComplexNumber scalar, Matrix mat1) {
            return mat1.Multiply(scalar);
        }

        public static Matrix operator *(Matrix mat1, ComplexNumber scalar) {
            return scalar * mat1;
        }

        public static Matrix operator /(Matrix mat1, ComplexNumber scalar) {
            return mat1.Divide(scalar);
        }

        public static Matrix operator +(Matrix mat1, Matrix mat2) {
            return mat1.Add(mat2);
        }

        public static Matrix operator -(Matrix mat1) {
            return -1 * mat1;
        }

        public static Matrix operator -(Matrix mat1, Matrix mat2) {
            return mat1.Subtract(mat2);
        }

        public static Matrix operator *(Matrix mat1, Matrix mat2) {
            return mat1.Multiply(mat2);
        }

        /// <summary>
        /// Converts the matrix to a human readable format.
        /// </summary>
        /// <returns>A string containing the elements of the matrix in a human readable format</returns>
        public override string ToString() {
            string toString = "";
            if (Height > 0 && Width > 0) {
                for (int i = 0; i < Height; i++) {
                    for (int j = 0; j < Width; j++) {
                        toString += this[i, j] + ", ";
                    }
                    toString = toString.Substring(0, toString.Length - 2);  // remove the ", "
                    toString += "\n";
                }
                toString = toString.Substring(0, toString.Length - 1);  // remove the "\n"
            }
            return toString;
        }

    }

    class MatrixException : Exception {
        public MatrixException(string message)
            : base(message) { }
    }

    class IncompatibleMatrixDimensionsException : MatrixException {
        public IncompatibleMatrixDimensionsException(Matrix mat1, Matrix mat2)
            : base("Incompatible matrix dimensions: " + mat1.Height + "x" + mat1.Width + " and " + mat2.Height + "x" + mat2.Width + ".") { }
    }
}
