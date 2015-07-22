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
        //private static int matrixSize = 5;

        public static void GenerateAll(int maxMatrixSize) {
            Random seed = new Random();

            GenerateRealMatrixFile(maxMatrixSize, seed);
            GenerateComplexMatrixFile(maxMatrixSize, seed);
            GenerateSymmetricMatrixFile(maxMatrixSize, seed);
            GenerateSymmetricPositiveDefinite(maxMatrixSize, seed);
            GenerateHermitianPositiveDefinite(maxMatrixSize, seed);
        }

        public static void GenerateRealMatrixFile(int maxMatrixSize, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(RM);

            int m = (int)Math.Ceiling((0.0 + maxMatrixSize) / 2.0);

            file.WriteLine(MatrixFactory.Real(1, maxMatrixSize, seed).ToStringAsInput());
            file.WriteLine(MatrixFactory.Real(maxMatrixSize, 1, seed).ToStringAsInput());
            file.WriteLine(MatrixFactory.Real(m, maxMatrixSize, seed).ToStringAsInput());
            file.WriteLine(MatrixFactory.Real(maxMatrixSize, m, seed).ToStringAsInput());
            file.WriteLine(MatrixFactory.Real(maxMatrixSize, maxMatrixSize, seed).ToStringAsInput());

            //for (int i = 0; i < nrOfMatrices; i++) {
            //    Matrix m = MatrixFactory.Real(matrixSize, matrixSize, seed);
            //    file.WriteLine(m.ToStringAsInput());
            //}

            file.Close();
        }

        public static void GenerateComplexMatrixFile(int maxMatrixSize, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(CM);

            int m = (int)Math.Ceiling((0.0 + maxMatrixSize) / 2.0);

            file.WriteLine(MatrixFactory.Complex(1, maxMatrixSize, seed).ToStringAsInput());
            file.WriteLine(MatrixFactory.Complex(maxMatrixSize, 1, seed).ToStringAsInput());
            file.WriteLine(MatrixFactory.Complex(m, maxMatrixSize, seed).ToStringAsInput());
            file.WriteLine(MatrixFactory.Complex(maxMatrixSize, m, seed).ToStringAsInput());
            file.WriteLine(MatrixFactory.Complex(maxMatrixSize, maxMatrixSize, seed).ToStringAsInput());

            //for (int i = 0; i < maxMatrixSize; i++) {
            //    Matrix m = MatrixFactory.Complex(maxMatrixSize, maxMatrixSize, seed);
            //    file.WriteLine(m.ToStringAsInput());
            //}

            file.Close();
        }

        public static void GenerateSymmetricMatrixFile(int maxMatrixSize, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(SM);

            int m = (int)Math.Ceiling((0.0 + maxMatrixSize) / 2.0);

            file.WriteLine(MatrixFactory.Symmetric(maxMatrixSize, seed).ToStringAsInput());

            //for (int i = 0; i < maxMatrixSize; i++) {
            //    Matrix m = MatrixFactory.Symmetric(maxMatrixSize, seed);
            //    file.WriteLine(m.ToStringAsInput());
            //}

            file.Close();
        }

        public static void GenerateSymmetricPositiveDefinite(int maxMatrixSize, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(SPM);

            int m = (int)Math.Ceiling((0.0 + maxMatrixSize) / 2.0);

            file.WriteLine(MatrixFactory.SymmetricPositiveDefinite(maxMatrixSize, seed).ToStringAsInput());

            //for (int i = 0; i < matrixSize; i++) {
            //    Matrix m = MatrixFactory.SymmetricPositiveDefinite(matrixSize, seed);
            //    file.WriteLine(m.ToStringAsInput());
            //}

            file.Close();
        }

        public static void GenerateHermitianPositiveDefinite(int maxMatrixSize, Random seed) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(HPM);

            int m = (int)Math.Ceiling((0.0 + maxMatrixSize) / 2.0);

            file.WriteLine(MatrixFactory.HermitianPositiveDefinite(maxMatrixSize, seed).ToStringAsInput());

            //for (int i = 0; i < maxMatrixSize; i++) {
            //    Matrix m = MatrixFactory.HermitianPositiveDefinite(maxMatrixSize, seed);
            //    file.WriteLine(m.ToStringAsInput());
            //}

            file.Close();
        }


    }
}
