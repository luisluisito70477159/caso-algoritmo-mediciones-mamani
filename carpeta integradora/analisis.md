//luis alberto mamani lopez

Nombre completo: Luis Alberto Mamani López

# I1 — Analizar y elegir

## a) Costo de `CompradorIngenuo.Elegir`

El costo de este algoritmo es **O(n · 2ⁿ)**, donde `n` es la cantidad de útiles.

**Explicación:**

El programa prueba todas las combinaciones posibles de útiles, es decir, cuáles comprar y cuáles no. Como cada útil tiene dos opciones, se generan muchas combinaciones. Después, el programa revisa cada combinación para calcular cuánto cuesta y qué utilidad obtiene. Por eso, mientras aumenta la cantidad de útiles, el tiempo que tarda también aumenta bastante.

## b) Recurrencia y teorema maestro

El algoritmo va revisando cada útil y tiene dos opciones: comprarlo o no comprarlo. Por eso, su recurrencia es:

$$
T(n) = 2T(n-1) + 1
$$

El teorema maestro no se puede aplicar directamente, porque esta fórmula trabaja reduciendo el problema de uno en uno, y no dividiéndolo entre un número como la mitad.

Al desarrollar la recurrencia, se obtiene un crecimiento exponencial:

$$
T(n) = \Theta(2^n)
$$

Esto significa que, al aumentar la cantidad de útiles, el algoritmo puede tardar mucho más en terminar. Además, en el código original se revisa cada combinación para calcular su costo y utilidad, por eso su costo total es **O(n · 2ⁿ)**.

## c) ¿Sirve un algoritmo voraz?

No siempre. El algoritmo voraz elige primero los útiles que parecen dar mayor utilidad por cada boliviano gastado, pero eso no significa que siempre encuentre la mejor combinación.

En el primer caso de prueba tenemos:

| Útil | Costo (Bs) | Utilidad | Utilidad/costo |
|------|------------:|---------:|---------------:|
| U1   | 20          | 120      | 6              |
| U2   | 40          | 200      | 5              |
| U3   | 60          | 240      | 4              |

**Presupuesto: 100 Bs.**

El método voraz elegiría primero U1 y después U2, porque tienen mayor utilidad por su costo. Gastaría 60 Bs y conseguiría una utilidad de 320. Ya no podría comprar U3 porque se pasaría del presupuesto.

Pero si se eligen U2 y U3, se gastan exactamente 100 Bs y se consigue una utilidad de 440, que es mayor.

Por eso, el método voraz no siempre encuentra la mejor solución.

**Estrategia elegida: programación dinámica.**

Elegí la programación dinámica porque permite ir guardando los mejores resultados que se van obteniendo, en lugar de probar todas las combinaciones desde el principio.

El programa revisa cada útil y compara si conviene comprarlo o dejarlo, tomando en cuenta el presupuesto disponible. De esta manera, puede encontrar la combinación que dé mayor utilidad sin repetir tantos cálculos.

Su costo depende de la cantidad de útiles y del presupuesto:

- **Tiempo:** O(n · P).
- **Espacio:** O(n · P).

Esta estrategia permite resolver el problema de una manera más rápida que el método original, especialmente cuando hay muchos útiles.

## d) Estructura para `EstaEnLaCompra`

Para esta parte usaría un `HashSet<string>`, porque permite guardar los códigos de los útiles que ya fueron seleccionados y comprobar fácilmente si uno está en la compra.

Con `Contains(codigo)` puedo verificar si un útil está dentro de la compra sin tener que recorrer toda una lista uno por uno.

Esto es más conveniente cuando se realizan muchas consultas, porque la búsqueda normalmente es más rápida que usando una lista.

