using System;

namespace VA_4
{
    class Program
    {
        private const double Eps = 1e-4;                    // Погрешность вычислений
        static void Main()
        {
            Console.WriteLine("Вариант-1");
            Console.WriteLine("Введите данные для решения нелинейного уравнения cos(x) - 4x = 0");
            Console.Write("Введите левую границу интервала: a = ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите правую границу интервала: b = ");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите шаг хождения по интервалу: h = ");
            double h = Convert.ToDouble(Console.ReadLine());
            double xLeft = a;               // Левая граница текущего интервала
            double xRight = xLeft + h;      // Правая граница текущего интервала
            double yLeft = Func(xLeft);     // Значение в левой границе текущего интервала
            double yRight;                  // Значение в правой границе текущего интервала
            bool find = false;              // Флаг существования решения
            while (xRight < b)
            {
                yRight = Func(xRight);
                // Console.WriteLine($"Текущий интервал: [{xLeft}; {xRight}]");
                // Console.WriteLine($"Значение в {xLeft}: {yLeft};\nЗначение в {xRight}: {yRight}");
                if (yLeft * yRight < 0)
                {
                    Console.WriteLine($"Текущий интервал: [{xLeft}; {xRight}]");
                    Console.WriteLine($"Значение в {xLeft}: {yLeft};\nЗначение в {xRight}: {yRight}");
                    find = true;            // Решение существует
                    // Уточнение решения методом половинного деления
                    Console.WriteLine($"x: {HalfDivision(xLeft, xRight)}");
                    // Уточнение решения методом Ньютона
                    Console.WriteLine($"x: {Newton(xLeft, xRight)}");
                }
                xLeft = xRight;
                xRight = xLeft + h;
                yLeft = yRight;
            }

            if (!find)
            {
                Console.WriteLine($"В интервале: [{a}; {b}] не существует решений уравнения cos(x) - 4x = 0");
            }
        }

        /// <summary>
        /// Метод половинного деления
        /// </summary>
        /// <param name="a">Левая граница текущего интервала</param>
        /// <param name="b">Правая граница текущего интервала</param>
        /// <returns>Приближенное решение уравнения</returns>
        static double HalfDivision(double a, double b)
        {
            double xLeft = a;   // Левая граница текущего интервала
            double xRight = b;  // Правая граница текущего интервала
            double x = (xLeft + xRight) / 2;  // Начальное приближение искомого корня
            int iterations = 0;
            Console.WriteLine("\tНомер итерации\tТекущий интервал\t\tТекущее приближение корня");
            while (Math.Abs(xRight - xLeft) > Eps && iterations < 20000)
            {
                Console.WriteLine($"\t{iterations}\t\t[{xLeft}; {xRight}]\t\t\t\t{x}");
                //Console.WriteLine($"\t{iterations} итерация.\tТекущий интервал:\t [{xLeft}; {xRight}]");
                //Console.WriteLine($"\t\t\tТекущее приближение: {x}");
                if (Func(xLeft) * Func(x) < 0)
                {
                    xRight = x;
                }
                else
                {
                    xLeft = x;
                }
                x = (xLeft + xRight) / 2;   // Приближение искомого корня
                iterations++;
            }
            Console.WriteLine($"При уточнении решения методом половинного деления было выполнено {iterations} итераций");
            return x;
        }

        /// <summary>
        /// Метод Ньютона
        /// </summary>
        /// <param name="a">Левая граница текущего интервала</param>
        /// <param name="b">Правая граница текущего интервала</param>
        /// <returns>Приближенное решение уравнения</returns>>
        static double Newton(double a, double b)
        {
            double xLeft = a;   // Левая граница текущего интервала
            double xRight = b;  // Правая граница текущего интервала
            double x = Func(xLeft) * Func2(xLeft) > 0 ? xLeft : xRight;  // Начальное приближение искомого корня
            int iterations = 0;
            double x1 = x - Func(x) / Func1(x);
            Console.WriteLine("\tНомер итерации\tПредыдущее приближение корня\tТекущее приближение корня");
            while (Math.Abs(x1 - x) > Eps && iterations < 20000)
            {
                Console.WriteLine($"\t{iterations}\t\t{x}\t\t\t\t{x1}");
                x = x1;
                x1 = x - Func(x) / Func1(x);
                iterations++;
            }
            Console.WriteLine($"При уточнении решения методом половинного деления было выполнено {iterations} итераций");
            return x;
        }

        /// <summary>
        /// Вычисление значения функции
        /// </summary>
        /// <param name="x">Аргумент</param>
        /// <returns>Значение функции</returns>
        static double Func(double x)
        {
            return Math.Cos(x) - 4 * x;
        }

        /// <summary>
        /// Вычисление значения первой производной
        /// </summary>
        /// <param name="x">Аргумент</param>
        /// <returns>Значение первой производной</returns>
        static double Func1(double x)
        {
            return -Math.Sin(x) - 4;
        }

        /// <summary>
        /// Вычисление значения второй производной
        /// </summary>
        /// <param name="x">Аргумент</param>
        /// <returns>Значение второй производной</returns>
        static double Func2(double x)
        {
            return -Math.Cos(x);
        }
    }
}
