
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Parcial2.VarianteA;

public interface IGestorTurnos
{
    void Registrar(string nombre, int urgencia);
    string? AtenderSiguiente();
    bool EstaEsperando(string nombre);
    int Esperando { get; }
}

public sealed record Paciente(string Nombre, int Urgencia, int Llegada);

// ---------- SOLUCION INGENUA ----------

public sealed class GestorIngenuo : IGestorTurnos
{
    private readonly List<Paciente> _lista = new();
    private int _llegadas;

    public void Registrar(string nombre, int urgencia) =>
        _lista.Add(new Paciente(nombre, urgencia, _llegadas++));

    public string? AtenderSiguiente()
    {
        if (_lista.Count == 0) return null;

        int mejor = 0;

        for (int i = 1; i < _lista.Count; i++)
        {
            var p = _lista[i];
            var m = _lista[mejor];

            if (p.Urgencia < m.Urgencia ||
                (p.Urgencia == m.Urgencia && p.Llegada < m.Llegada))
                mejor = i;
        }

        string nombre = _lista[mejor].Nombre;
        _lista.RemoveAt(mejor);

        return nombre;
    }

    public bool EstaEsperando(string nombre)
    {
        foreach (var p in _lista)
            if (p.Nombre == nombre) return true;

        return false;
    }

    public int Esperando => _lista.Count;
}

// ---------- SOLUCION MEJORADA ----------

public sealed class GestorMejorado : IGestorTurnos
{
    private readonly PriorityQueue<Paciente, (int Urgencia, int Llegada)> _cola = new();

    private readonly Dictionary<string, int> _nombres = new();

    private int _llegadas;

    public void Registrar(string nombre, int urgencia)
    {
        var paciente = new Paciente(nombre, urgencia, _llegadas++);

        _cola.Enqueue(
            paciente,
            (urgencia, paciente.Llegada)
        );

        if (_nombres.ContainsKey(nombre))
            _nombres[nombre]++;
        else
            _nombres[nombre] = 1;
    }

    public string? AtenderSiguiente()
    {
        if (_cola.Count == 0)
            return null;

        Paciente paciente = _cola.Dequeue();

        if (_nombres[paciente.Nombre] == 1)
            _nombres.Remove(paciente.Nombre);
        else
            _nombres[paciente.Nombre]--;

        return paciente.Nombre;
    }

    public bool EstaEsperando(string nombre)
    {
        return _nombres.ContainsKey(nombre);
    }

    public int Esperando => _cola.Count;
}

// ---------- PROGRAMA PRINCIPAL ----------

public static class Programa
{
    private static readonly (string op, string arg, int urg, string esperado)[] Guion =
    {
        ("registrar", "Ana", 3, ""),
        ("registrar", "Beto", 1, ""),
        ("registrar", "Carla", 3, ""),
        ("registrar", "Diego", 2, ""),
        ("esperando", "", 0, "4"),
        ("esta", "Beto", 0, "True"),
        ("esta", "Zoe", 0, "False"),
        ("atender", "", 0, "Beto"),
        ("esta", "Beto", 0, "False"),
        ("atender", "", 0, "Diego"),
        ("registrar", "Elena", 1, ""),
        ("atender", "", 0, "Elena"),
        ("atender", "", 0, "Ana"),
        ("atender", "", 0, "Carla"),
        ("atender", "", 0, "(nadie)"),
        ("esperando", "", 0, "0"),
    };

    private static string Ejecutar(
        IGestorTurnos g,
        string op,
        string arg,
        int urg)
    {
        switch (op)
        {
            case "registrar":
                g.Registrar(arg, urg);
                return "";

            case "atender":
                return g.AtenderSiguiente() ?? "(nadie)";

            case "esta":
                return g.EstaEsperando(arg).ToString();

            default:
                return g.Esperando.ToString();
        }
    }

    private static void Probar(string titulo, IGestorTurnos g)
    {
        int bien = 0, total = 0;

        foreach (var (op, arg, urg, esperado) in Guion)
        {
            string obtenido = Ejecutar(g, op, arg, urg);

            if (op == "registrar") continue;

            total++;

            if (obtenido == esperado)
                bien++;
            else
                Console.WriteLine(
                    $"MAL en {op} {arg}: esperado {esperado}, obtuvo {obtenido}"
                );
        }

        Console.WriteLine($"{titulo}: {bien} de {total} pasos correctos");
    }

    // ---------- CARGA DE TRABAJO ----------

    public static long EjecutarCarga(IGestorTurnos g, int n)
    {
        for (int i = 0; i < n; i++)
            g.Registrar("P" + i, (int)((i * 7919L) % 5) + 1);

        long control = 0;

        for (int i = 0; i < n; i++)
            control += g.AtenderSiguiente()!.Length;

        return control;
    }

    // ---------- MEDICION DE TIEMPOS ----------

    private static double Medir(Func<long> accion)
    {
        accion(); // Calentamiento

        double suma = 0;

        for (int i = 0; i < 3; i++)
        {
            var reloj = Stopwatch.StartNew();

            long resultado = accion();

            reloj.Stop();

            suma += reloj.Elapsed.TotalMilliseconds;

            Console.WriteLine(
                $"Repeticion {i + 1}: {reloj.Elapsed.TotalMilliseconds:F2} ms | Control: {resultado}"
            );
        }

        return suma / 3;
    }

    public static void Main()
    {
        Console.WriteLine("===== PRUEBAS DE CORRECCION =====");

        Probar("GestorIngenuo", new GestorIngenuo());
        Probar("GestorMejorado", new GestorMejorado());

        Console.WriteLine();

        Console.WriteLine("===== MEDICION DE RENDIMIENTO =====");

        int n = 10000;

        Console.WriteLine("\nGESTOR INGENUO:");

        double tiempoIngenuo = Medir(
            () => EjecutarCarga(new GestorIngenuo(), n)
        );

        Console.WriteLine("\nGESTOR MEJORADO:");

        double tiempoMejorado = Medir(
            () => EjecutarCarga(new GestorMejorado(), n)
        );

        Console.WriteLine("\n===== RESULTADOS FINALES =====");

        Console.WriteLine($"Pacientes: {n}");

        Console.WriteLine($"Promedio ingenuo:
