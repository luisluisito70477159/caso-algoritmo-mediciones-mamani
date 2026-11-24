
using System;
using System.Diagnostics;

class Paciente
{
    public int Codigo { get; set; }
    public int Urgencia { get; set; }
    public int OrdenLlegada { get; set; }
}

class Program
{
    // Regla de prioridad:
    // 1. Menor número de urgencia.
    // 2. Si empatan, menor orden de llegada.
    static bool TieneMayorPrioridad(Paciente a, Paciente b)
    {
        if (a.Urgencia != b.Urgencia)
            return a.Urgencia < b.Urgencia;

        return a.OrdenLlegada < b.OrdenLlegada;
    }

    // ALGORITMO 1: FUERZA BRUTA O(n²)
    // Cada paciente se compara con todos los demás.
    static Paciente FuerzaBruta(Paciente[] pacientes)
    {
        Paciente ganador = null;

        for (int i = 0; i < pacientes.Length; i++)
        {
            bool esGanador = true;

            for (int j = 0; j < pacientes.Length; j++)
            {
                if (i == j)
                    continue;

                if (TieneMayorPrioridad(pacientes[j], pacientes[i]))
                    esGanador = false;
            }

            if (esGanador)
                ganador = pacientes[i];
        }

        return ganador;
    }

    // ALGORITMO 2: VORAZ O(n)
    // Recorre una vez y conserva al mejor candidato.
    static Paciente Voraz(Paciente[] pacientes)
    {
        if (pacientes.Length == 0)
            return null;

        Paciente mejor = pacientes[0];

        for (int i = 1; i < pacientes.Length; i++)
        {
            if (TieneMayorPrioridad(pacientes[i], mejor))
                mejor = pacientes[i];
        }

        return mejor;
    }

    // Genera pacientes con urgencias aleatorias de 1 a 5.
    // El orden de llegada se conserva mediante el índice.
    static Paciente[] GenerarPacientes(int cantidad, int semilla)
    {
        Random random = new Random(semilla);
        Paciente[] pacientes = new Paciente[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            pacientes[i] = new Paciente
            {
                Codigo = i + 1,
                Urgencia = random.Next(1, 6),
                OrdenLlegada = i + 1
            };
        }

        return pacientes;
    }

    // Mide una ejecución en milisegundos.
    static double Medir(Action accion)
    {
        Stopwatch reloj = Stopwatch.StartNew();
        accion();
        reloj.Stop();

        return reloj.Elapsed.TotalMilliseconds;
    }

    // Promedio de 3 ejecuciones.
    static double Promedio(double[] tiempos)
    {
        double suma = 0;

        foreach (double tiempo in tiempos)
            suma += tiempo;

        return suma / tiempos.Length;
    }

    static void Main()
    {
        int[] tamanos = { 1000, 2000, 4000, 8000 };

        double anteriorBruta = 0;
        double anteriorVoraz = 0;

        Console.WriteLine("H3 - KARDEX MEDICO SEPSA");
        Console.WriteLine("Seleccion del paciente prioritario");
        Console.WriteLine();

        Console.WriteLine(
            "Pacientes | Fuerza Bruta ms | Factor | Voraz ms | Factor");

        foreach (int n in tamanos)
        {
            Paciente[] pacientes = GenerarPacientes(n, 12345);

            // Verificación: ambos deben seleccionar al mismo paciente.
            Paciente resultadoBruto = FuerzaBruta(pacientes);
            Paciente resultadoVoraz = Voraz(pacientes);

            if (resultadoBruto.Codigo != resultadoVoraz.Codigo)
            {
                Console.WriteLine("ERROR: los resultados no coinciden.");
                return;
            }

            Console.WriteLine(
                $"Verificacion {n}: ambos seleccionan al paciente " +
                $"{resultadoVoraz.Codigo}, urgencia " +
            $"{resultadoVoraz.Urgencia}.");

            // Calentamiento: no se registra este tiempo.
            FuerzaBruta(pacientes);
            Voraz(pacientes);

            double[] tiemposBruta = new double[3];
            double[] tiemposVoraz = new double[3];

            // Tres mediciones de fuerza bruta.
            for (int r = 0; r < 3; r++)
            {
                tiemposBruta[r] = Medir(() =>
                {
                    FuerzaBruta(pacientes);
                });
            }

            // Tres mediciones del algoritmo voraz.
            for (int r = 0; r < 3; r++)
            {
                tiemposVoraz[r] = Medir(() =>
                {
                    Voraz(pacientes);
                });
            }

            double promedioBruta = Promedio(tiemposBruta);
            double promedioVoraz = Promedio(tiemposVoraz);

            string factorBruta = anteriorBruta == 0
                ? "-"
                : (promedioBruta / anteriorBruta).ToString("F2");

            string factorVoraz = anteriorVoraz == 0
                ? "-"
                : (promedioVoraz / anteriorVoraz).ToString("F2");

            Console.WriteLine(
                $"{n} | {promedioBruta:F4} | {factorBruta} | " +
                $"{promedioVoraz:F4} | {factorVoraz}");

            anteriorBruta = promedioBruta;
            anteriorVoraz = promedioVoraz;
        }

        Console.WriteLine();
        Console.WriteLine("Pruebas finalizadas.");
    }
}




H3 - KARDEX MEDICO SEPSA
Seleccion del paciente prioritario

Pacientes | Fuerza Bruta ms | Factor | Voraz ms | Factor

Verificacion 1000: ambos seleccionan ...
1000 | ... | - | ... | -

Verificacion 2000: ambos seleccionan ...
2000 | ... | ... | ... | ...

Verificacion 4000: ambos seleccionan ...
4000 | ... | ... | ... | ...

Verificacion 8000: ambos seleccionan ...
8000 | ... | ... | ... | ...

Pruebas finalizadas.
