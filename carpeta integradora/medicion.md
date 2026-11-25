//Luis Alberto Mamani lopez 

# Evaluación integradora – Variante B

## Medición de rendimiento

### 1\. Protocolo

Se utilizó el método `EjecutarCarga` para generar datos reproducibles a partir de una semilla basada en el tamaño n. Para cada tamaño se realizó una ejecución de calentamiento y luego tres ejecuciones medidas. Se calculó el promedio de los tres tiempos.

Los algoritmos se ejecutaron con los mismos datos generados para cada tamaño. Los tiempos están expresados en milisegundos (ms) y corresponden a la salida obtenida en esta computadora.

### 2\. Resultados: ingenuo y mejorado

|n|Ingenuo (ms)|Mejorado (ms)|Cociente ingenuo/mejorado|
|-:|-:|-:|-:|
|16|13.47|0.04|310.44x|
|18|47.74|0.05|881.35x|
|20|177.20|0.06|3064.02x|
|22|694.42|0.06|12197.11x|

El mayor tamaño probado, n = 22, tardó 694.42 ms (aproximadamente 0.694 segundos) con el algoritmo ingenuo, dentro del intervalo solicitado de 0.5 a 20 segundos.

### 3\. Crecimiento entre tamaños consecutivos

Se compara el tiempo ingenuo de cada fila con el de la fila anterior:

|Comparación|Cociente de tiempos ingenuos|
|-|-:|
|18 / 16|3.54x|
|20 / 18|3.71x|
|22 / 20|3.92x|

Los cocientes muestran que el tiempo del algoritmo ingenuo crece rápidamente al aumentar el número de artículos. Los valores no tienen que ser idénticos entre mediciones porque el tiempo real depende también de la computadora y de la ejecución.

### 4\. Medición con 1000 elementos

Para el algoritmo mejorado, con 1000 elementos y presupuesto de Bs 10.000, el promedio de las tres ejecuciones medidas fue:

**178.92 ms.**

### 5\. Análisis y conclusión

En los tamaños probados, el algoritmo mejorado fue considerablemente más rápido que el ingenuo. La diferencia se hace mayor a medida que aumenta n, lo cual concuerda con el crecimiento exponencial de la fuerza bruta frente al costo pseudo-polinomial de la programación dinámica respecto del presupuesto.

Los resultados son mediciones de una computadora y una ejecución concretas. Pueden variar por el procesador, la memoria, la carga del sistema operativo, el calentamiento del entorno .NET y otros factores. Los tiempos muy pequeños del algoritmo mejorado también son sensibles a esas variaciones; por ello se utilizó el promedio de tres ejecuciones y se deben interpretar como resultados experimentales, no como tiempos universales.

### 6\. Presupuesto muy grande

Con un presupuesto de Bs 1.000.000.000, la tabla de programación dinámica indexada por cada valor del presupuesto podría consumir demasiada memoria. La complejidad O(n · P) depende directamente del valor de P, por lo que esta implementación no es adecuada para presupuestos numéricos tan grandes.


