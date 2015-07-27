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
        public const string SQM = PATH + "square_matrices.txt";
        public const string SM = PATH + "symmetric_matrices.txt";
        public const string SPM = PATH + "symmetric_positive_matrices.txt";
        public const string HPM = PATH + "hermitian_positive_matrices.txt";

        public static void GenerateAll(int maxMatrixSize, int minNrOfMatrices) {
            Random seed = new Random();

            GenerateRealMatrixFile(maxMatrixSize, minNrOfMatrices, seed);
            GenerateComplexMatrixFile(maxMatrixSize, minNrOfMatrices, seed);
            GenerateSquareMatrixFile(maxMatrixSize, minNrOfMatrices, seed);
            GenerateSymmetricMatrixFile(maxMatrixSize, minNrOfMatrices, seed);
            GenerateSymmetricPositiveDefinite(maxMatrixSize, minNrOfMatrices, seed);
            GenerateHermitianPositiveDefinite(maxMatrixSize, minNrOfMatrices, seed);
        }

        public static void GenerateRealMatrixFile(int maxMatrixSize, int minNrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(RM);

            int m = (int)Math.Ceiling((0.0 + maxMatrixSize) / 2.0);

            for (int i = 0; i < minNrOfMatrices; i++) {
                file.WriteLine(MatrixFactory.Real(1, maxMatrixSize, seed).ToStringAsInput());
                file.WriteLine(MatrixFactory.Real(maxMatrixSize, 1, seed).ToStringAsInput());
                file.WriteLine(MatrixFactory.Real(m, maxMatrixSize, seed).ToStringAsInput());
                file.WriteLine(MatrixFactory.Real(maxMatrixSize, m, seed).ToStringAsInput());
                file.WriteLine(MatrixFactory.Real(maxMatrixSize, maxMatrixSize, seed).ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateComplexMatrixFile(int maxMatrixSize, int minNrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(CM);

            int m = (int)Math.Ceiling((0.0 + maxMatrixSize) / 2.0);

            for (int i = 0; i < minNrOfMatrices; i++) {
                file.WriteLine(MatrixFactory.Complex(1, maxMatrixSize, seed).ToStringAsInput());
                file.WriteLine(MatrixFactory.Complex(maxMatrixSize, 1, seed).ToStringAsInput());
                file.WriteLine(MatrixFactory.Complex(m, maxMatrixSize, seed).ToStringAsInput());
                file.WriteLine(MatrixFactory.Complex(maxMatrixSize, m, seed).ToStringAsInput());
                file.WriteLine(MatrixFactory.Complex(maxMatrixSize, maxMatrixSize, seed).ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateSquareMatrixFile(int maxMatrixSize, int minNrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(SQM);

            for (int i = 0; i < minNrOfMatrices; i++) {
                file.WriteLine(MatrixFactory.Square(maxMatrixSize, seed).ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateSymmetricMatrixFile(int maxMatrixSize, int minNrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(SM);

            for (int i = 0; i < minNrOfMatrices; i++) {
                file.WriteLine(MatrixFactory.Symmetric(maxMatrixSize, seed).ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateSymmetricPositiveDefinite(int maxMatrixSize, int minNrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(SPM);

            for (int i = 0; i < minNrOfMatrices; i++) {
                file.WriteLine(MatrixFactory.SymmetricPositiveDefinite(maxMatrixSize, seed).ToStringAsInput());
            }

            file.Close();
        }

        public static void GenerateHermitianPositiveDefinite(int maxMatrixSize, int minNrOfMatrices, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(HPM);

            for (int i = 0; i < minNrOfMatrices; i++) {
                file.WriteLine(MatrixFactory.HermitianPositiveDefinite(maxMatrixSize, seed).ToStringAsInput());
            }

            file.Close();
        }


    }
}
