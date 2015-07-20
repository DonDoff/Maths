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
