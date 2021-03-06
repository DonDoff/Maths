﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Maths;
using MathsTest;

namespace ConsoleApplication {
    class Program {
        static void Main(string[] args) {
            TestMatrixGenerator.GenerateAll(5, 1);

            Matrix A; 

            //A = MatrixFactory.ParseFrom("4, 2, 2, 1, 5; 2, -3, 1, 1, 3; 2, 1, 3, 1, 4; 1, 1, 1, 2, 3; 1, 2, 6, 4, 1");
            //A = MatrixFactory.ParseFrom("1, 0, 0, 0, 2; 0, 0, 3, 0, 0; 0, 0, 0, 0, 0; 0, 4, 0, 0, 0");
            A = MatrixFactory.ParseFrom("1, 1; 0, 1; 1, -1; -1, 2i");
            //A = MatrixFactory.ParseFrom("1, 1, 1, 1; 1, 1, -1, 1; 1, 1, 1, -1");
            //A = MatrixFactory.ParseFrom("1; 1; 3");
            //A = MatrixFactory.ParseFrom("1, 1, 3");
            //A = MatrixFactory.ParseFrom("4, 2, i; 2, -3, 1; 3, 2, 5; 2, 5, 1");
            //A = MatrixFactory.ParseFrom("4, 2, i; 2, -3, 1; 3, 2, 5; 2, 5, 1; 2, 2i, 1+i");
            //A = MatrixFactory.ParseFrom("1, 1; 1, 2; 1, 3; 1, 4; 1, 5");
            //A = MatrixFactory.ParseFrom("4, 3, 0, 2; 2, 1, 2, 1; 4, 4, 0, 3");
            //A = MatrixFactory.ParseFrom("1, -1, 4; 1, 4, -2; 1, 4, 2; 1, -1, 0");
            //A = MatrixFactory.Real(3, 3, new Random());

            A = A.Transpose();
            Console.WriteLine("A:\n" + A + "\n");
             
            //HessenbergDecomposition hessen = new HessenbergDecomposition(A);
            //Matrix P = hessen.P;
            //Console.WriteLine("H:\n" + hessen.H + "\n");
            //Console.WriteLine("P:\n" + hessen.P + "\n");


            //Matrix mActual, mExpected;
            //EigenvalueDecomposition eigen;
            //eigen = A.Eigen();


            //for (int i = 0; i < A.Height; i++) {
            //    mActual = A * eigen.Eigenvectors.GetColumnVector(i) - eigen.Eigenvalues[i] * eigen.Eigenvectors.GetColumnVector(i);
            //    mExpected = MatrixFactory.Zeros(A.Height, 1);
            //    Console.WriteLine("A * v (" + i + "):\n" + A * eigen.Eigenvectors.GetColumnVector(i) + "\n");
            //    Console.WriteLine("lambda * v (" + i + "):\n" + eigen.Eigenvalues[i] * eigen.Eigenvectors.GetColumnVector(i) + "\n");
            //}


            //QRDecomposition qr = A.QR();
            //Console.WriteLine("Q:\n" + qr.Q + "\n");
            //Console.WriteLine("R:\n" + qr.R + "\n");
            //Console.WriteLine("QR:\n" + qr.Q * qr.R + "\n");


            //ColumnVector b = ColumnVector.ParseFrom("6, 5, 7, 10, 15");
            //Console.WriteLine("b:\n" + b + "\n");
            //SolveLinearEquations sle = new SolveLinearEquations(A);
            //ColumnVector x = sle.Solve(b);
            //Console.WriteLine("x:\n" + x + "\n");
            //ColumnVector xx = A.GetColumnVector(1);
            //ColumnVector y = x[0] * A.GetColumnVector(0) + x[1] * xx;
            //Plot.CreateMathsPlotWindow(new List<ColumnVector> { xx, xx }, new List<ColumnVector> { b, y });

            //Bidiagonalization bid = A.Bidiagonalization();
            //Console.WriteLine("U:\n" + bid.U + "\n");
            //Console.WriteLine("B:\n" + bid.B + "\n");
            //Console.WriteLine("V:\n" + bid.V + "\n");
            //Console.WriteLine("UBV:\n" + bid.U * bid.B * bid.V.ConjugateTranspose() + "\n");

            
            SVD svd = A.SVD();
            Console.WriteLine("U:\n" + svd.U + "\n");
            Console.WriteLine("UT*U:\n" + svd.U.ConjugateTranspose() * svd.U + "\n");
            Console.WriteLine("D:\n" + svd.D + "\n");
            Console.WriteLine("V:\n" + svd.V + "\n");
            Console.WriteLine("UDV.T:\n" + svd.U * svd.D * svd.V.ConjugateTranspose() + "\n");
 
            Console.WriteLine("U?:\n" + svd.U.IsUnitary() + "\n");
            Console.WriteLine("D?:\n" + svd.D.IsDiagonal() + "\n");
            Console.WriteLine("V?:\n" + svd.V.IsUnitary() + "\n");




            //int N = 1000;
            //double tStart = 0;
            //double tEnd = 10;
            //double Fs = (N + 0.0) / (tEnd - tStart);

            //Vector t = Vector.Linspace(0, 10, N);
            ////Vector t = Vector.Linspace(0, (N + 0.0) / Fs, N);
            ////Vector t = Vector.Arrange(-2, 2, (0.0 + Fs) / N);
            //double freq = 1;

            //Vector ones = Vector.Ones(N);
            //Vector exp = MatrixMath.Exp(-Math.PI * t.ElementMultiply(t)).ToColumnVector();
            //Vector noise = Vector.RandomComplex(N, new Random());
            //Vector deltaPulse = Vector.Ei(N, N / 2);
            //Vector fftSin = Vector.Ei(N / 2, N / 4).ConcatenateRows(Vector.Ei(N / 2, N / 4)).ToColumnVector();
            //Vector step = Vector.Zeros(N / 2).ConcatenateRows(Vector.Ones(N / 2)).ToColumnVector();
            //Vector rect = Vector.Zeros(N / 4).ConcatenateRows(Vector.Ones(N / 2)).ConcatenateRows(Vector.Zeros(N / 4)).ToColumnVector();
            //Vector ramp = Vector.Arrange(N);

            //Vector sin = MatrixMath.Sin(2 * Math.PI * freq * t).ToColumnVector();
            //Vector sin3 = MatrixMath.Sin(3 * 2 * Math.PI * freq * t).ToColumnVector();
            //Vector sin5 = MatrixMath.Sin(5 * 2 * Math.PI * freq * t).ToColumnVector();
            //Vector cos = MatrixMath.Cos(2 * Math.PI * freq * t).ToColumnVector();
            //Vector sinh = MatrixMath.Sinh(2 * Math.PI * freq * t).ToColumnVector();
            //Vector specialCos = (MatrixMath.Cos(2 * Math.PI * 3 * t).ElementMultiply(MatrixMath.Exp(-Math.PI * t.ElementMultiply(t)))).ToColumnVector();
            //Vector sinc = MatrixMath.Sinc(2 * Math.PI * freq * t).ToColumnVector();

            //Vector y = sin5;
            //Vector fft = FFT.CalculateFFT(y);

            ////Vector y2 = ones;
            ////Vector fft2 = FFT.CalculateFFT(y2);

            //Vector f = Vector.Linspace(-Fs / 2, Fs / 2, fft.Size);

            //Plot.CreateMathsPlotWindow(t, y);
            //Plot.CreateMathsPlotWindow(f, VectorMath.FFTNormalize(VectorMath.FFTShift(fft)));
        }


    }
}
