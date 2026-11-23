# Medición – Kardex médico SEPSA

## Protocolo
- Se compara la misma mezcla de operaciones y las mismas claves en `List<int>` y `SortedSet<int>`.
- Tamaños de entrada: 1.000, 2.000, 4.000 y 8.000 pacientes (se duplican).
- Se realiza una ejecución de calentamiento que no se registra.
- Se miden 3 repeticiones y se reporta el promedio.
- El tiempo se toma con `Stopwatch` en milisegundos. El tiempo de creación inicial de las estructuras queda fuera de la medición.
- Ejecutar en la misma computadora, sin otros procesos pesados, y en modo Release.

## Resultados
**Completa esta tabla con los valores que imprime tu programa. No inventes tiempos:** pueden variar según la computadora, el sistema operativo y los procesos que estén ejecutándose.

| Pacientes (n) | Lista ingenua promedio (ms) | `SortedSet` promedio (ms) | Lista: veces más que fila anterior | `SortedSet`: veces más que fila anterior |
|---:|---:|---:|---:|---:|
| 1.000 | — | — | — | — |
| 2.000 | — | — | — | — |
| 4.000 | — | — | — | — |
| 8.000 | — | — | — | — |

## Conclusión (rellenar con tus resultados)
La lista ingenua tardó más/menos al crecer el número de pacientes, mientras que `SortedSet` mostró ______. Esto coincide/no coincide con los costos esperados: búsqueda e inserción/eliminación en lista O(n) frente a O(log n) en `SortedSet`. Los tiempos pueden variar por el equipo y por ruido del sistema; por eso se calentó el programa, se repitió tres veces y se promedió.

## Cómo ejecutar
Desde la carpeta que contiene el proyecto:

```bash
dotnet run -c Release
```

Copia los cuatro resultados del programa a la tabla de este documento y redacta la conclusión según lo observado.
