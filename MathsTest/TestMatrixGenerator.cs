using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maths;

namespace MathsTest {
    public static class TestMatrixGenerator {

        private const string PATH = "C:\\Users\\adoff\\Documents\\Visual Studio 2013\\Projects\\Maths\\MathsTest\\TestMatrices\\";
        public const string RM = PATH + "real_matrices.txt";
        public const string CM = PATH + "complex_matrices.txt";
        public const string SM = PATH + "symmetric_matrices.txt";
        public const string SPM = PATH + "symmetric_positive_matrices.txt";
        public const string HPM = PATH + "hermitian_positive_matrices.txt";
        private static int matrixSize = 5;

        public static void GenerateAll(int nrOfMatrices) {
            Random seed = new Random();

            GenerateRealMatrixFile(nrOfMatrices, seed);
            GenerateComplexMatrixFile(nrOfMatrices, seed);
            GenerateSymmetricMatrixFile(nrOfMatrices, seed);
            GenerateSymmetricPositiveDefinite(nrOfMatrices, seed);
            GenerateHermitianPositiveDefinite(nrOfMatrices, seed);
        }

        public static void GenerateRealMatrixFile(int nrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(RM);

            for (int i = 0; i < nrOfMatrices; i++) {
                Matrix m = MatrixFactory.Real(matrixSize, matrixSize, seed);
                file.WriteLine(m.ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateComplexMatrixFile(int nrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(CM);

            for (int i = 0; i < nrOfMatrices; i++) {
                Matrix m = MatrixFactory.Complex(matrixSize, matrixSize, seed);
                file.WriteLine(m.ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateSymmetricMatrixFile(int nrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(SM);

            for (int i = 0; i < nrOfMatrices; i++) {
                Matrix m = MatrixFactory.Symmetric(matrixSize, seed);
                file.WriteLine(m.ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateSymmetricPositiveDefinite(int nrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(SPM);

            for (int i = 0; i < nrOfMatrices; i++) {
                Matrix m = MatrixFactory.SymmetricPositiveDefinite(matrixSize, seed);
                file.WriteLine(m.ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateHermitianPositiveDefinite(int nrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(HPM);

            for (int i = 0; i < nrOfMatrices; i++) {
                Matrix m = MatrixFactory.HermitianPositiveDefinite(matrixSize, seed);
                file.WriteLine(m.ToStringAsInput());
            }

            file.Close();
        }


    }
}
