using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using NUnit.Framework;
using Maths;

namespace MathsTest {

    public class TestMatrices {
        
        public static IEnumerable RealMatrices {
            get {
                string[] matrixStrings = System.IO.File.ReadAllLines(TestMatrixGenerator.RM);
                foreach (string matrixString in matrixStrings) {
                    yield return new TestCaseData(MatrixFactory.ParseFrom(matrixString));
                }
            }
        }

        public static IEnumerable ComplexMatrices {
            get {
                string[] matrixStrings = System.IO.File.ReadAllLines(TestMatrixGenerator.CM);
                foreach (string matrixString in matrixStrings) {
                    yield return new TestCaseData(MatrixFactory.ParseFrom(matrixString));
                }
            }
        }

        public static IEnumerable SquareMatrices {
            get {
                string[] matrixStrings = System.IO.File.ReadAllLines(TestMatrixGenerator.SQM);
                foreach (string matrixString in matrixStrings) {
                    yield return new TestCaseData(MatrixFactory.ParseFrom(matrixString));
                }
            }
        }

        public static IEnumerable SymmetricMatrices {
            get {
                string[] matrixStrings = System.IO.File.ReadAllLines(TestMatrixGenerator.SM);
                foreach (string matrixString in matrixStrings) {
                    yield return new TestCaseData(MatrixFactory.ParseFrom(matrixString));
                }
            }
        }

        public static IEnumerable SymmetricPositiveDefiniteMatrices {
            get {
                string[] matrixStrings = System.IO.File.ReadAllLines(TestMatrixGenerator.SPM);
                foreach (string matrixString in matrixStrings) {
                    yield return new TestCaseData(MatrixFactory.ParseFrom(matrixString));
                }
            }
        }

        public static IEnumerable HermitianPositiveDefiniteMatrices {
            get {
                string[] matrixStrings = System.IO.File.ReadAllLines(TestMatrixGenerator.HPM);
                foreach (string matrixString in matrixStrings) {
                    yield return new TestCaseData(MatrixFactory.ParseFrom(matrixString));
                }
            }
        }
    }
}
