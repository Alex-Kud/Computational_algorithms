using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;

namespace Gauss
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Введите количество уравнений: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите начальное значение диапазона коэфициентов и свободных членов системы: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите конечное значение диапазона коэфициентов и свободных членов системы: ");
            int b = Convert.ToInt32(Console.ReadLine());
            var matrixA = new double[n, n];             // Коэффициенты
            var matrixB = new double[n];                // Свободные члены
            Random rnd = new Random();
            // Заполнение коэффициентов
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    matrixA[i, j] = rnd.Next(a, b);
                }
            }

            // Заполнение свободных членов
            for (int i = 0; i < n; ++i)
                matrixB[i] = rnd.Next(a, b);

            Show(matrixA, matrixB, n);
            var result = Gauss(matrixA, matrixB, n);
            for (int i = 0; i < n; ++i)
                Console.WriteLine(i + ": " + result[i]);
        }

        private static void Show(double[,] matrixA, double[] matrixB, int n)
        {
            Console.WriteLine("Матрица");
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write(Math.Round(matrixA[i, j], 4) + "*x" + j);
                    if (j < n - 1)
                        Console.Write("\t+ ");
                }
                Console.WriteLine("\t= " + Math.Round(matrixB[i], 4));
            }
        }

        private static double[] Gauss(double[,] matrixA, double[] matrixB, int n)
        {
            var matrixX = new double[n];
            int k = 0; // Номер шага
            // Прямой ход – приведение СЛАУ к треугольному виду
            while (k < n)
            {
                Console.WriteLine("\n\t\t\tШаг " + k + '\n');
                // Деление k-того уравнения на ведущий коэфициент
                double temp = matrixA[k, k];
                if (Math.Abs(temp) == 0) continue; // для нулевого коэффициента пропустить
                for (int j = 0; j < n; j++)
                    matrixA[k, j] = matrixA[k, j] / temp;
                matrixB[k] = matrixB[k] / temp;

                // Исключение переменной xk. Вычитание из k+1-ого уравнения k-того уравнения, умноженного на ведущий коэфициент k+1-ого 
                for (int i = k; i < n; i++)
                {
                    double temp2 = matrixA[i, k];
                    if (i == k)
                    {
                        Console.WriteLine("i==k");
                        Show(matrixA, matrixB, n); 
                        continue; // уравнение не вычитать само из себя
                    }
                    for (int j = 0; j < n; j++)
                        matrixA[i, j] = matrixA[i, j] - matrixA[k, j]*temp2;
                    matrixB[i] = matrixB[i] - matrixB[k]*temp2;
                    Show(matrixA, matrixB, n);
                }
                k++;
            }

            // Обратный ход Гаусса
            for (k = n - 1; k >= 0; k--)
            {
                double s = 0;
                for (int j = n - 1; j > k; j--)
                {
                    s += matrixX[j] * matrixA[k, j];
                }
                matrixX[k] = (matrixB[k] - s);
            }

            return matrixX;
        }
    }
}
