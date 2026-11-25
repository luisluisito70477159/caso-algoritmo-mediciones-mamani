//Luis Alberto Mamani lopez

# Evaluación integradora – Variante B

## Análisis del problema: Compras de una escuela

### 1\. Algoritmo ingenuo

El método `CompradorIngenuo.Elegir` revisa todas las combinaciones posibles de artículos. Para n artículos existen 2^n subconjuntos. Para cada subconjunto recorre hasta n artículos para calcular su costo y utilidad.

Por ello, su complejidad temporal es **O(n · 2^n)**. El espacio adicional utilizado para recorrer las combinaciones es O(1), sin contar la lista que guarda los artículos elegidos.

### 2\. Recurrencia

La recurrencia indicada es:

T(n) = 2T(n − 1) + 1

El teorema maestro clásico se aplica directamente a recurrencias de la forma T(n) = aT(n/b) + f(n). Aquí el tamaño del subproblema es n − 1, no n/b; por eso no corresponde aplicarlo directamente.

Desarrollando la recurrencia:

* T(n) = 2T(n − 1) + 1
* T(n) = 2\[2T(n − 2) + 1] + 1 = 4T(n − 2) + 3
* Al continuar, T(n) = 2^n T(0) + (2^n − 1).

Por tanto, su orden es **Θ(2^n)** para el modelo de recurrencia. En la implementación ingenua, además se examinan hasta n artículos por subconjunto, de modo que el costo total es **O(n · 2^n)**.

### 3\. Estrategia voraz y contraejemplo

No conviene elegir siempre el artículo con mayor utilidad por boliviano, porque esa decisión local no garantiza la mejor combinación global.

En el primer caso:

* U1: costo Bs 20, utilidad 120, razón 6.
* U2: costo Bs 40, utilidad 200, razón 5.
* U3: costo Bs 60, utilidad 240, razón 4.
* Presupuesto: Bs 100.

La estrategia voraz por razón elige U1 y U2, con costo Bs 60 y utilidad 320. Sin embargo, U2 y U3 cuestan Bs 100 y producen utilidad 440. Por tanto, la elección voraz no es óptima.

La estrategia utilizada es **programación dinámica para el problema de la mochila 0/1**. Se calcula la mejor utilidad posible considerando los primeros i artículos y cada presupuesto p. Así se reutilizan resultados de subproblemas.

### 4\. Complejidad del algoritmo mejorado

La tabla tiene (n + 1) filas y (P + 1) columnas, donde P es el presupuesto en bolivianos. Cada celda se calcula en tiempo constante.

* Tiempo: **O(n · P)**.
* Espacio: **O(n · P)**.
* La reconstrucción de los artículos elegidos recorre las filas en O(n).

Esta complejidad es pseudo-polinomial, porque depende del valor numérico del presupuesto, no solamente de la cantidad de artículos.

### 5\. Estructura para consultar artículos elegidos

`CompradorMejorado` utiliza `HashSet<string>` para almacenar los códigos seleccionados. La búsqueda mediante `EstaEnLaCompra` tiene costo promedio **O(1)**. Si se utilizara una lista, la búsqueda sería O(n) en el peor caso.

### 6\. Presupuesto de Bs 1.000.000.000

Con un presupuesto de mil millones de bolivianos, una tabla de programación dinámica por presupuesto sería impracticable. Por ejemplo, con 1000 artículos se necesitarían aproximadamente 1001 × 1.000.000.001 celdas. Usando enteros de 4 bytes, esto representa alrededor de 4 TB, sin contar sobrecargas.

Para presupuestos tan grandes se debería considerar otra estrategia, como programación dinámica con estados dispersos, una formulación por valores cuando corresponda o algoritmos aproximados, según las restricciones del problema.

### 7\. Conclusión

La fuerza bruta encuentra la solución exacta, pero su crecimiento exponencial la vuelve lenta al aumentar los artículos. La programación dinámica evita recalcular subproblemas y mejora considerablemente el rendimiento para presupuestos moderados. El `HashSet` también permite consultar rápidamente si un código pertenece a la compra.

