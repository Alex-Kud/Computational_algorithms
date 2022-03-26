using System;

namespace laba_2
{
    class Program
    {
        private const decimal PI = 3.14159265358979323846m;
        private const decimal EPS = 1e-17m;                    // Погрешность вычислений
        static void Main(string[] args)
        {
            try
            {
                // a * (P(x))^2 * sin(b) - R(t) * e^z
                Console.Write("Введите число a - double число: ");
                decimal a = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("Введите многочлен P:");
                Console.Write("Введите степень n многочлена P - целое число: ");
                int nP = Convert.ToInt32(Console.ReadLine());
                nP++;                                        // Количество коэффициентов
                Console.Write("Введите значение переменной x - десятичная дробь: ");
                decimal x = Convert.ToDecimal(Console.ReadLine());
                int[] arrP = new int[nP];
                for (int i = 0; i < nP; ++i)
                {
                    Console.Write($"Введите коэффициент перед x^{nP - i - 1} - int число: ");
                    arrP[i] = Convert.ToInt32(Console.ReadLine());
                }

                Console.Write("Введите аргумент синуса (в градусах) - целое число: ");
                int b = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Введите рациональную дробь R:");
                Console.Write("Введите значение t в рациональной дроби - десятичная дробь: ");
                decimal t = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Введите многочлен-числитель рациональной дроби R:");
                Console.Write("Введите степень n многочлена-числителя R - целое число: ");
                int nR1 = Convert.ToInt32(Console.ReadLine());
                nR1++;                                        // Количество коэффициентов
                int[] arrR1 = new int[nR1];
                for (int i = 0; i < nR1; ++i)
                {
                    Console.Write($"Введите коэффициент перед t^{nR1 - i - 1} - int число: ");
                    arrR1[i] = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("Введите многочлен-знаменатель рациональной дроби R:");
                Console.Write("Введите степень n многочлена-знаменателя R - целое число: ");
                int nR2 = Convert.ToInt32(Console.ReadLine());
                nR2++;                                        // Количество коэффициентов
                int[] arrR2 = new int[nR2];
                for (int i = 0; i < nR2; ++i)
                {
                    Console.Write($"Введите коэффициент перед t^{nR2 - i - 1} - int число: ");
                    arrR2[i] = Convert.ToInt32(Console.ReadLine());
                }

                Console.Write("Введите степень z показательной функции - десятичная дробь: ");
                decimal z = Convert.ToDecimal(Console.ReadLine());

                // a * (P(x))^2 * sin(b) - R(t) * e^z
                // Результаты промежуточных вычислений
                decimal P = Gorner(nP, x, arrP);
                decimal P2 = P * P;
                decimal sinB = Sin(b);
                decimal R = RationalFraction(t, nR1, arrR1, nR2, arrR2);
                decimal e = Exp(z);
                decimal result = a * P2 * sinB - R * e;
                
                // Вывод результатов промежуточных вычислений:
                Console.WriteLine($"Значение многочлена P({x}): {Math.Round(P, 8)}");
                Console.WriteLine($"Значение P({x})^2: {Math.Round(P2, 8)}");
                Console.WriteLine($"Значение sin({b}): {Math.Round(sinB, 8)}");
                Console.WriteLine($"Значение рациональной дроби R({t}): {Math.Round(R, 8)}");
                Console.WriteLine($"Значение показательной функции e^{z}: {Math.Round(e, 8)}");
                Console.WriteLine($"Значение выражения {a} * (P({x}))^2 * sin({b}) - R({t}) * e^{z}: {Math.Round(result, 8)}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ввод чиселки некорректен :(");
            }
        }

        /// <summary>
        /// Вычисление значения многочлена по схеме Горнера
        /// </summary>
        /// <param name="n">Степень многочлена</param>
        /// <param name="x">Значение переменной</param>
        /// <param name="arr">Массив коэффициентов от высшей степени к низшей</param>
        /// <returns>Значение многочлена</returns>
        private static decimal Gorner(int n, decimal x, int[] arr)
        {
            decimal s = arr[0];
            for (int i = 1; i < n; ++i)
                s = s * x + arr[i];
            //s = Math.Round(s, 8);       // Округление до 8 знаков
            return s;
        }

        /// <summary>
        /// Вычисление рациональной дроби
        /// </summary>
        /// <param name="x">Значение переменной</param>
        /// <param name="n1">Степень многочлена числителя</param>
        /// <param name="arr1">Массив коэффициентов числителя от высшей степени к низшей</param>
        /// <param name="n2">Степень многочлена знаменателя</param>
        /// <param name="arr2">Массив коэффициентов знаменателя от высшей степени к низшей</param>
        /// <returns>Значение рациональной дроби</returns>
        private static decimal RationalFraction(decimal x, int n1, int[] arr1, int n2, int[] arr2)
        {
            return Gorner(n1, x, arr1) / Gorner(n2, x, arr2);
        }

        /// <summary>
        /// Вычисление синуса
        /// </summary>
        /// <param name="x">Значение аргумента</param>
        /// <returns>Значение синуса</returns>
        private static decimal Sin(decimal x)
        {
            x *= PI / 180;              // Перевод из градусов в радианы
            decimal s = 0;               // Начальная сумма
            decimal q = x;               // Первый член ряда
            int n = 1;                  // Показатель степени
            while (Math.Abs(q) > EPS)   // Условие выполнения цикла
            {
                s += q;                 // Добавление члена ряда
                q = -q * x * x / ((2 * n) * (2 * n + 1));  // Новый член ряда 
                n++;                    // Наращивание  n:  1,2,3,4,...
            }

            //s = Math.Round(s, 8);       // Округление до 8 знаков
            return s;                   // Возврат результата
        }

        /// <summary>
        /// Вычисление значения показательной функции
        /// </summary>
        /// <param name="x">Значение аргумента</param>
        /// <returns>Значение показательной функции</returns>
        private static decimal Exp(decimal x)
        {
            decimal s = 0;               // Начальная сумма
            decimal q = 1;               // Первый член ряда
            int n = 1;                  // Показатель степени
            while (Math.Abs(q) > EPS)   // Условие выполнения цикла
            {
                s += q;                 // Добавление члена ряда
                q = q * x / n;          // Новый член ряда 
                n++;                    // Наращивание  n:  1,2,3,4,...
            }

            //s = Math.Round(s,8);        // Округление до 8 знаков
            return s;                   // Возврат результата
        }
    }
}
