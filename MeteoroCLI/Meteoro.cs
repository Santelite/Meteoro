using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MeteoroCLI
{
    public class Meteoro
    {
        double sesgo = 0.0;
        double razon = 0.1;
        double[] pesas = { 0.5, -0.5, 0.5 };

        public void Predecir()
        {
            double[][] entrada =
                { 
                //eventualmente habrá que cargar desde un archivo pq estos numeros magicos no me gustan.
                //en cualquier caso, estos son datos de ejemplo, cada fila del array es un día y cada uno son la media de la temperatura de todo dia.
                //los días irian del más antiguo (index 0) al más reciente (index 3).
                    new double[] { 20, 22, 24 }, // ~26
                    new double[] { 22, 24, 26 }, // ~28
                    new double[] { 24, 26, 28 }, // ~30
                    new double[] { 10, 12, 14 }  // ~16
                };

            double[] esperado = { 26, 28, 30, 16 };

            for (int n = 0; n < esperado.Length; n++)
            {
                esperado[n] = esperado[n] / 100.0;
            }

            /*
            foreach(double esp in esperado)
            {
                Console.WriteLine(esp);
            }
            */

            Console.Write("Ingrese la cantidad de generaciones para el entrenamiento: (Se recomiendan al menos 10,000)");
            if (int.TryParse(Console.ReadLine(), out int generaciones))
                Console.WriteLine($"Generaciones establecidas en {generaciones}");
            else
            {
                generaciones = 10000;
                Console.WriteLine($"Entrada inválida. Generaciones establecidas en valor por defecto: {generaciones}");
            }

            Console.WriteLine("Iniciando entrenamiento...");

            for (int g = 0; g < generaciones; g++)
            {
                double errorTotal = 0.0;

                for (int i = 0; i < entrada.Length; i++)
                {
                    double t1 = entrada[i][0] / 100.0;
                    double t2 = entrada[i][1] / 100.0;
                    double t3 = entrada[i][2] / 100.0;
                    double verdadero = esperado[i];

                    double suma = (t1 * pesas[0]) + (t2 * pesas[1]) + (t3 * pesas[2]) + sesgo;

                    double prediccion = 1 / (1 + Math.Exp(-suma));

                    double error = Math.Pow((prediccion - verdadero), 2); //error cuadrático medio
                    errorTotal += error;

                    double deltaErrorPrediccion = 2 * (prediccion - verdadero); //derivada del error cuadrático medio
                    double deltaPrediccionSuma = prediccion * (1 - prediccion); //derivada de la función sigmoide
                    double delta = deltaErrorPrediccion * deltaPrediccionSuma;

                    double deltaPeso1 = delta * t1;
                    double deltaPeso2 = delta * t2;
                    double deltaPeso3 = delta * t3;
                    double deltaSesgo = delta * 1;

                    pesas[0] -= razon * deltaPeso1;
                    pesas[1] -= razon * deltaPeso2;
                    pesas[2] -= razon * deltaPeso3;
                    sesgo -= razon * deltaSesgo;
                }

                if (g % 1000 == 0)
                    Console.WriteLine($"Generación {g} completada, error total {errorTotal:F6}");
            }

            Console.WriteLine("Entrenamiento completado.");

            // probemos

            /*
            double p1 = 20 / 100.0, p2 = 22 / 100.0, p3 = 24 / 100.0;
            */

            double p1 = Console.ReadLine() is string entrada1 && double.TryParse(entrada1, out double temp1) ? temp1 / 100.0 : 0.0;
            double p2 = Console.ReadLine() is string entrada2 && double.TryParse(entrada2, out double temp2) ? temp2 / 100.0 : 0.0;
            double p3 = Console.ReadLine() is string entrada3 && double.TryParse(entrada3, out double temp3) ? temp3 / 100.0 : 0.0;

            double final = (p1 * pesas[0]) + (p2 * pesas[1]) + (p3 * pesas[2]) + sesgo;
            double prediccionFinal = 1 / (1 + Math.Exp(-final));

            Console.WriteLine($"Predicción para temperaturas ingresadas: {prediccionFinal * 100.0:F1}°C");
            Console.WriteLine($"Preddición sin normalizar: {prediccionFinal}");
        }
    }
}
