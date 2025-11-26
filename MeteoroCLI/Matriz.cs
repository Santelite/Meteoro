using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoroCLI
{
    using System;

    public class Matriz
    {
        public int Filas { get; private set; }
        public int Columna { get; private set; }
        public double[,] Data;

        public Matriz(int filas, int cols)
        {
            Filas = filas;
            Columna = cols;
            Data = new double[filas, cols];
        }

        public void Inicializar()
        {
            Random rand = new Random();
            for (int i = 0; i < Filas; i++)
                for (int j = 0; j < Columna; j++)
                    //valor random de -1 a 1 a cada "campo" de la matriz
                    Data[i, j] = rand.NextDouble() * 2 - 1;
        }

        //producto punto de dos matrices
        public static Matriz Punto(Matriz a, Matriz b)
        {
            if (a.Columna != b.Filas)
                throw new Exception("Matrices no cuadra");

            Matriz resultado = new Matriz(a.Filas, b.Columna);

            for (int i = 0; i < resultado.Filas; i++) // fila de a
            {
                for (int j = 0; j < resultado.Columna; j++) // columna de b
                {
                    double sum = 0;
                    for (int k = 0; k < a.Columna; k++)
                    {
                        sum += a.Data[i, k] * b.Data[k, j];
                    }
                    resultado.Data[i, j] = sum;
                }
            }
            return resultado;
        }

        public static Matriz Transpuesta(Matriz m)
        {
            Matriz result = new Matriz(m.Columna, m.Filas);
            for (int i = 0; i < m.Filas; i++)
                for (int j = 0; j < m.Columna; j++)
                    result.Data[j, i] = m.Data[i, j]; //pense que esto sería mas complicado xd, pero tiene sentido
            return result;
        }

        // aca comienzan las operaciones "normales"
        public static Matriz SumarMat(Matriz a, Matriz b)
        {
            Matriz result = new Matriz(a.Filas, a.Columna);
            for (int i = 0; i < a.Filas; i++)
                for (int j = 0; j < a.Columna; j++)
                    result.Data[i, j] = a.Data[i, j] + b.Data[i, j];
            return result;
        }

        public static Matriz RestarMat(Matriz a, Matriz b)
        {
            Matriz result = new Matriz(a.Filas, a.Columna);
            for (int i = 0; i < a.Filas; i++)
                for (int j = 0; j < a.Columna; j++)
                    result.Data[i, j] = a.Data[i, j] - b.Data[i, j];
            return result;
        }

        public static Matriz Multiplicar(Matriz a, double scalar) // multiplicacion escalar
        {
            Matriz result = new Matriz(a.Filas, a.Columna);
            for (int i = 0; i < a.Filas; i++)
                for (int j = 0; j < a.Columna; j++)
                    result.Data[i, j] = a.Data[i, j] * scalar;
            return result;
        }

        public static Matriz Multiplicar(Matriz a, Matriz b) // multiplicacion con otra matriz
        {
            Matriz result = new Matriz(a.Filas, a.Columna);
            for (int i = 0; i < a.Filas; i++)
                for (int j = 0; j < a.Columna; j++)
                    result.Data[i, j] = a.Data[i, j] * b.Data[i, j];
            return result;
        }


        // algo para "aplicar" funciones a cada elemento de la matriz
        public Matriz Funcion(Func<double, double> func)
        {
            Matriz result = new Matriz(Filas, Columna);
            for (int i = 0; i < Filas; i++)
                for (int j = 0; j < Columna; j++)
                    result.Data[i, j] = func(Data[i, j]);
            return result;
        }

        // imprimir toda la matriz
        public void ImprimirMat()
        {
            for (int i = 0; i < Filas; i++)
            {
                for (int j = 0; j < Columna; j++)
                    Console.Write($"{Data[i, j]:F4}\t");
                Console.WriteLine();
            }
        }

        // convertir un array a una matriz pq los datos de open meteo vienen asi
        public static Matriz ConvArray(double[] arr)
        {
            Matriz m = new Matriz(arr.Length, 1);
            for (int i = 0; i < arr.Length; i++)
                m.Data[i, 0] = arr[i];
            return m;
        }
    }
}
