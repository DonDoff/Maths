using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public static class MatrixFactory {

        /// <summary>
        /// Tries to create a matrix by parsing the specified matrix string.
        /// </summary>
        /// <param name="matString"></param>
        /// <returns></returns>
        public static Matrix ParseFrom(string matString) {
            matString = matString.Replace(" ", "");

            string[] rows = matString.Split(';');
            Matrix newMat = new Matrix(rows.Length, rows[0].Split(',').Length);

            try {
                for (int i = 0; i < newMat.Height; i++) {
                    string[] columns = rows[i].Split(',');
                    for (int j = 0; j < newMat.Width; j++) {
                        newMat[i, j] = new Complex(columns[j]);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return newMat;
        }

        /// <summary>
        /// Create an identity matrix of the specified size.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Matrix IdentityMatrix(int size) {
            Matrix newMat = new Matrix(size, size);
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    newMat[i, j] = 0;
                }
                newMat[i, i] = 1;
            }
            return newMat;
        }

        /// <summary>
        /// Create a matrix containing zeros of the specified height and width.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Matrix Zeros(int height, int width) {
            return new Matrix(height, width);
        }

        /// <summary>
        /// Create a matrix containing ones of the specified height and width.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Matrix Ones(int height, int width) {
            return new Matrix(height, width).AppyToAllElements(x => 1);
        }

        /// <summary>
        /// Fill a new matrix with the element c
        /// </summary>
        /// <param name="m"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Matrix Fill(Matrix m, Complex c) {
            return m.AppyToAllElements(x => c);
        }

        /// <summary>
        /// Create a matrix with random complex numbers.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Matrix Complex(int height, int width, Random seed = null) {
            return new Matrix(height, width).AppyToAllElements((x) => ComplexMath.Rand(seed));
        }

        /// <summary>
        /// Create a matrix with random real numbers.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Matrix Real(int height, int width, Random seed = null) {
            return Complex(height, width, seed).RealPart();
        }

        /// <summary>
        /// Create a matrix with random pure imaginary numbers.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Matrix Imaginary(int height, int width, Random seed = null) {
            return Complex(height, width, seed).ImPart();
        }

        /// <summary>
        /// Create a square matrix with random complex numbers.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Matrix Square(int size, Random seed = null) {
            return Complex(size, size, seed);
        }

        /// <summary>
        /// Create an n x n Hankel matrix based on the specified first and last column.
        /// </summary>
        /// <param name="firstColumn"></param>
        /// <param name="lastColumn"></param>
        /// <returns></returns>
        public static Matrix Hankel(Vector firstColumn, Vector lastColumn) {
            if (firstColumn.Size != lastColumn.Size) {
                throw new IncompatibleMatrixDimensionsException(firstColumn, lastColumn);
            }
            if (firstColumn[firstColumn.Size - 1] != lastColumn[0]) {
                throw new MatrixException("The last and first index of the specified vectors don't match!");
            }

            Vector concat = firstColumn.ConcatenateRows(lastColumn[Vector.Arrange(1, lastColumn.Size)]).ToColumnVector();

            Matrix newMat = new Matrix(firstColumn.Size);
            for (int i = 0; i < firstColumn.Size; i++) {
                newMat[Vector.Arrange(firstColumn.Size), i] = concat.SubMatrix(i, i + firstColumn.Size, 0, 1).ToColumnVector();
            }
            return newMat;
        }

        /// <summary>
        /// Create an n x n Toeplitz matrix based on the specified first row and column.
        /// </summary>
        /// <param name="firstRow"></param>
        /// <param name="firstColumn"></param>
        /// <returns></returns>
        public static Matrix Toeplitz(Vector firstRow, Vector firstColumn) {
            if (firstRow.Size != firstColumn.Size) {
                throw new IncompatibleMatrixDimensionsException(firstRow, firstColumn);
            }
            if (firstRow[0] != firstColumn[0]) {
                throw new MatrixException("The first indices of the specified vectors don't match!");
            }

            Matrix newMat = new Matrix(firstRow.Size);
            for (int i = 0; i < firstRow.Size; i++) {
                newMat[firstRow.Size - i - 1, Vector.Arrange(firstRow.Size - i - 1, firstRow.Size)] = firstRow.SubMatrix(0, i + 1, 0, 1).ToColumnVector();
            }

            for (int i = 1; i < firstColumn.Size; i++) {
                newMat[i, Vector.Arrange(i)] = firstColumn.SubMatrix(1, i + 1, 0, 1).ToColumnVector();
            }

            return newMat;
        }

        /// <summary>
        /// Create a random Hermitian matrix containing complex numbers.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Matrix Hermitian(int size, Random seed = null) {
            Matrix m = new Matrix(size, size).AppyToAllElements((x) => ComplexMath.Rand(seed));

            for (int i = 0; i < size; i++) {
                m[i, i] = m[i, i].R;    // The diagonal needs to be real.
                for (int j = 0; j < i; j++) {
                    m[i, j] = m[j, i].Conjugate();
                }
            }

            return m;
        }

        /// <summary>
        /// Create a random symmetric matrix containing real numbers.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Matrix Symmetric(int size, Random seed = null) {
            return Hermitian(size, seed).RealPart();
        }

        /// <summary>
        /// Create a random unitary matrix.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Matrix Unitary(int size, Random seed = null) {
            Matrix m = Complex(size, size, seed);
            return m.QR().Q;
        }

        /// <summary>
        /// Create a random orthogonal matrix.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Matrix Orthogonal(int size, Random seed = null) {
            Matrix m = Complex(size, size, seed).RealPart();
            return m.QR().Q;
        }

        public static Matrix HermitianPositiveSemiDefinite(int size, Random seed = null) {
            Matrix m = Complex(size, size, seed);
            return m.ConjugateTranspose() * m;
        }

        public static Matrix HermitianPositiveDefinite(int size, Random seed = null) {
            return HermitianPositiveSemiDefinite(size, seed) + IdentityMatrix(size);
        }

        public static Matrix SymmetricPositiveSemiDefinite(int size, Random seed = null) {
            Matrix m = Real(size, size, seed);
            return m.ConjugateTranspose() * m;
        }

        public static Matrix SymmetricPositiveDefinite(int size, Random seed = null) {
            return SymmetricPositiveSemiDefinite(size, seed) + IdentityMatrix(size);
        }
    }
}
