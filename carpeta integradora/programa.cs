//luis alberto mamani lopez
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Integradora.VarianteB
{
    public sealed record Util(string Codigo, int CostoBs, int Utilidad);

    public interface IComprador
    {
        (int utilidad, List<string> codigos) Elegir(
            Util[] utiles, int presupuestoBs);

        bool EstaEnLaCompra(string codigo);
    }

    // ALGORITMO INGENUO: fuerza bruta
    public sealed class CompradorIngenuo : IComprador
    {
        private List<string> elegidos = new();

        public (int utilidad, List<string> codigos) Elegir(
            Util[] utiles, int presupuestoBs)
        {
            int n = utiles.Length;
            int mejor = 0;
            long mejorMascara = 0;

            if (n >= 63)
                throw new ArgumentException("El algoritmo ingenuo admite menos de 63 elementos.");

            for (long mascara = 0; mascara < (1L << n); mascara++)
            {
                int costo = 0;
                int utilidad = 0;

                for (int i = 0; i < n; i++)
                {
                    if ((mascara & (1L << i)) != 0)
                    {
                        costo += utiles[i].CostoBs;
                        utilidad += utiles[i].Utilidad;
                    }
                }

                if (costo <= presupuestoBs && utilidad > mejor)
                {
                    mejor = utilidad;
                    mejorMascara = mascara;
                }
            }

            elegidos.Clear();

            for (int i = 0; i < n; i++)
            {
                if ((mejorMascara & (1L << i)) != 0)
                    elegidos.Add(utiles[i].Codigo);
            }

            return (mejor, new List<string>(elegidos));
        }

        public bool EstaEnLaCompra(string codigo)
        {
            return elegidos.Contains(codigo);
        }
    }

    // ALGORITMO MEJORADO: programación dinámica
    public sealed class CompradorMejorado : IComprador
    {
        private HashSet<string> elegidos = new();

        public (int utilidad, List<string> codigos) Elegir(
            Util[] utiles, int presupuestoBs)
        {
            if (presupuestoBs < 0)
                throw new ArgumentException("El presupuesto no puede ser negativo.");

            int n = utiles.Length;
            int[,] dp = new int[n + 1, presupuestoBs + 1];

            elegidos.Clear();

            for (int i = 1; i <= n; i++)
            {
                int costo = utiles[i - 1].CostoBs;
                int utilidad = utiles[i - 1].Utilidad;

                for (int p = 0; p <= presupuestoBs; p++)
                {
                    dp[i, p] = dp[i - 1, p];

                    if (costo <= p)
                    {
                        int alternativa = dp[i - 1, p - costo] + utilidad;

                        if (alternativa > dp[i, p])
                            dp[i, p] = alternativa;
                    }
                }
            }

            int restante = presupuestoBs;

            for (int i = n; i >= 1; i--)
            {
                if (dp[i, restante] != dp[i - 1, restante])
                {
                    elegidos.Add(utiles[i - 1].Codigo);
                    restante -= utiles[i - 1].CostoBs;
                }
            }

            return (dp[n, presupuestoBs], elegidos.ToList());
        }

        public bool EstaEnLaCompra(string codigo)
        {
            return elegidos.Contains(codigo);
        }
    }

    public static class Programa
    {
        private static Util[] Lote(
            params (string c, int costo, int u)[] datos)
        {
            return datos.Select(d =>
                new Util(d.c, d.costo, d.u)).ToArray();
        }

        private static readonly
        (string que, Util[] utiles, int presupuesto,
        int esperada, string[] codigos)[] Casos =
        {
            ("el voraz se equivoca",
                Lote(("U1",20,120),("U2",40,200),("U3",60,240)),
                100, 440, new[] {"U2","U3"}),

            ("cuatro utiles",
                Lote(("U1",4,6),("U2",6,8),("U3",8,10),("U4",10,12)),
                16, 20, new[] {"U2","U4"}),

            ("presupuesto cero",
                Lote(("U1",5,10),("U2",7,12)),
                0, 0, Array.Empty<string>()),

            ("sin utiles",
                Lote(),
                30, 0, Array.Empty<string>()),

            ("nada cabe",
                Lote(("U1",80,90),("U2",100,100)),
                60, 0, Array.Empty<string>()),

            ("entran todos",
                Lote(("U1",10,20),("U2",14,24),("U3",6,8)),
                200, 52, new[] {"U1","U2","U3"})
        };

        // EJECUCION DE CARGA PARA MEDICIONES
        public static int EjecutarCarga(IComprador comprador, int n)
        {
            var azar = new Random(n + 1000);

            var utiles = Enumerable.Range(0, n)
                .Select(i => new Util(
                    "K" + i,
                    azar.Next(4, 50),
                    azar.Next(10, 100)))
                .ToArray();

            int presupuesto = utiles.Sum(u => u.CostoBs) / 3;

            var (utilidad, _) = comprador.Elegir(utiles, presupuesto);

            return utilidad;
        }

        // PRUEBAS AUTOMATICAS
        private static void Probar(string titulo, Func<IComprador> fabrica)
        {
            int bien = 0;
            int total = 0;

            foreach (var caso in Casos)
            {
                var comprador = fabrica();

                var (utilidad, elegidos) =
                    comprador.Elegir(caso.utiles, caso.presupuesto);

                total++;

                bool ok = utilidad == caso.esperada &&
                    elegidos.OrderBy(x => x)
                    .SequenceEqual(caso.codigos.OrderBy(x => x));

                if (ok)
                    bien++;
                else
                    Console.WriteLine("ERROR: " + caso.que);
            }

            var c = fabrica();
            c.Elegir(Casos[0].utiles, Casos[0].presupuesto);

            total += 2;

            if (c.EstaEnLaCompra("U2"))
                bien++;

            if (!c.EstaEnLaCompra("U1"))
                bien++;

            Console.WriteLine(titulo + ": " + bien +
                " de " + total + " pruebas correctas");
        }

        // MEDICION: UN CALENTAMIENTO Y TRES EJECUCIONES
        private static double Medir(IComprador comprador, int n)
        {
            EjecutarCarga(comprador, n);

            double suma = 0;

            for (int i = 0; i < 3; i++)
            {
                var reloj = Stopwatch.StartNew();

                EjecutarCarga(comprador, n);

                reloj.Stop();
                suma += reloj.Elapsed.TotalMilliseconds;
            }

            return suma / 3.0;
        }

        private static void MedirAlgoritmos()
        {
            Console.WriteLine();
            Console.WriteLine("MEDICION DE RENDIMIENTO");
            Console.WriteLine("Tiempos promedio en milisegundos");
            Console.WriteLine();

           
            int[] tamanos = { 16, 18, 20, 22 };

            Console.WriteLine("n\tIngenuo\tMejorado\tVelocidad");

            double anterior = 0;

            foreach (int n in tamanos)
            {
                double tiempoIngenuo =
                    Medir(new CompradorIngenuo(), n);

                double tiempoMejorado =
                    Medir(new CompradorMejorado(), n);

                double velocidad = tiempoMejorado > 0
                    ? tiempoIngenuo / tiempoMejorado
                    : 0;

                Console.WriteLine(
                    n + "\t" +
                    tiempoIngenuo.ToString("F2") + "\t" +
                    tiempoMejorado.ToString("F2") + "\t" +
                    velocidad.ToString("F2") + "x");

                anterior = tiempoIngenuo;
            }

            Console.WriteLine();
            Console.WriteLine("MEDICION CON 1000 ELEMENTOS");

            var azar = new Random(1234);

            var utiles = Enumerable.Range(0, 1000)
                .Select(i => new Util(
                    "K" + i,
                    azar.Next(4, 50),
                    azar.Next(10, 100)))
                .ToArray();

            var mejorado = new CompradorMejorado();

            mejorado.Elegir(utiles, 10000);

            double suma = 0;

            for (int i = 0; i < 3; i++)
            {
                var reloj = Stopwatch.StartNew();
                mejorado.Elegir(utiles, 10000);
                reloj.Stop();
                suma += reloj.Elapsed.TotalMilliseconds;
            }

            Console.WriteLine(
                "Promedio con 1000 elementos y presupuesto 10000: " +
                (suma / 3.0).ToString("F2") + " ms");

            Console.WriteLine();
            Console.WriteLine(
                "Con presupuesto de 1000000000 Bs, la tabla de programacion dinamica puede requerir demasiada memoria.");
        }

        public static void Main()
        {
            Console.WriteLine("EVALUACION INTEGRADORA - VARIANTE B");
            Console.WriteLine();

            Probar("CompradorIngenuo", () => new CompradorIngenuo());

            Probar("CompradorMejorado", () => new CompradorMejorado());

            MedirAlgoritmos();

            Console.WriteLine();
            Console.WriteLine("Fin de las pruebas.");
        }
    }
}
