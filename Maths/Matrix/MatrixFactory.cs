using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths {
    public static class MatrixFactory {

        public static Matrix ParseFrom(string matString) {
            matString = matString.Replace(" ", "");

            string[] rows = matString.Split(';');
            Matrix newMat = new Matrix(rows.Length, rows[0].Split(',').Length);

            try {
                for (int i = 0; i < newMat.Height; i++) {
                    string[] columns = rows[i].Split(',');
                    for (int j = 0; j < newMat.Width; j++) {
                        newMat[i, j] = new ComplexNumber(columns[j]);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return newMat;
        }

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

        // Fill the matrix with random complex numbers
        public static Matrix RandomComplex(int rows, int columns, Random seed = null) {
            if (seed == null) {
                seed = new Random();
            }

            Matrix newMat = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    newMat[i, j] = new ComplexNumber(seed.NextDouble(), seed.NextDouble());
                }
            }
            return newMat;
        }

        // Fill the matrix with random real numbers
        public static Matrix RandomReal(int rows, int columns, Random seed = null) {
            if (seed == null) {
                seed = new Random();
            }

            Matrix newMat = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    newMat[i, j] = seed.NextDouble();
                }
            }
            return newMat;
        }

        public static Matrix Symmetric(int size, Random seed = null) {
            if (seed == null) {
                seed = new Random();
            }

            Matrix newMat = new Matrix(size);
            for (int i = 0; i < size; i++) {
                for (int j = i; j < size; j++) {
                    newMat[i, j] = seed.NextDouble();
                }
            }
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < i; j++) {
                    newMat[i, j] = newMat[j, i];
                }
            }

            return newMat;
        }

        public static Matrix Hankel(int size, Vector firstColumn, Vector lastColumn) {
            if (size != firstColumn.Size || size != lastColumn.Size) {
                throw new MatrixException("The specified size and vector sizes don't match!");
            }
            if (firstColumn[firstColumn.Size - 1] != lastColumn[0]) {
                throw new MatrixException("The last and first index of the specified vectors don't match!");
            }

            Vector concat = firstColumn.ConcatenateRows(lastColumn[Vector.Arrange(1, lastColumn.Size)]).ToColumnVector();
            
            Matrix newMat = new Matrix(size);
            for (int i = 0; i < size; i++) {
                newMat[Vector.Arrange(size), i] = concat.SubMatrix(i, i+size, 0, 1).ToColumnVector();
            }
            return newMat;
        }

        public static Matrix Zeros(int rows, int columns) {
            return new Matrix(rows, columns);
        }

        public static Matrix Ones(int rows, int columns) {
            return new Matrix(rows, columns).AppyToAllElements(x => 1);
        }

        // Fill a new matrix will the element c
        public static Matrix Fill(Matrix m, ComplexNumber c) {
            return m.AppyToAllElements(x => c);
        }
    }
}
