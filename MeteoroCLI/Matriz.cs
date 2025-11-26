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
                    //valor random de -1 a 1
                    Data[i, j] = rand.NextDouble() * 2 - 1;
        }

        //producto punto de dos matrices
        public static Matriz Punto(Matriz a, Matriz b)
        {
            if (a.Columna != b.Filas)
                throw new Exception("Matrices no cuadra");

            Matriz result = new Matriz(a.Filas, b.Columna);

            for (int i = 0; i < result.Filas; i++) // fila de a
            {
                for (int j = 0; j < result.Columna; j++) // fila de b
                {
                    double sum = 0;
                    for (int k = 0; k < a.Columna; k++)
                    {
                        sum += a.Data[i, k] * b.Data[k, j];
                    }
                    result.Data[i, j] = sum;
                }
            }
            return result;
        }

        
    }
}
