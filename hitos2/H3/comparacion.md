
# H3 – Comparación de algoritmos: Kardex médico SEPSA

## 1. Problema

En el consultorio médico de SEPSA pueden existir varios pacientes esperando atención. Se necesita seleccionar al paciente que debe ser atendido primero, tomando en cuenta su urgencia y el orden de llegada.

Reglas:
- La urgencia 1 tiene mayor prioridad que 2, 3, 4 y 5.
- Si dos pacientes tienen la misma urgencia, se elige al que llegó primero.

Se comparará una solución por fuerza bruta con una solución mediante recorrido voraz.

## 2. Algoritmo por fuerza bruta

La fuerza bruta examina cada paciente como posible ganador y lo compara con los demás. Si encuentra otro paciente que tiene prioridad superior, descarta al candidato. Si ningún otro paciente tiene mayor prioridad, ese candidato es el ganador.

Para representar el caso de peor crecimiento, el algoritmo revisa las comparaciones necesarias sin detenerse anticipadamente al encontrar un paciente mejor.

Su complejidad temporal en el peor caso es **O(n²)**, porque realiza comparaciones repetidas entre pacientes.

## 3. Algoritmo voraz

El algoritmo voraz recorre la lista una sola vez y conserva al paciente que, hasta ese momento, tiene la mayor prioridad. Si encuentra otro paciente con mayor prioridad, actualiza el candidato. Al terminar, devuelve el paciente prioritario.

La elección local consiste en conservar al mejor paciente encontrado hasta ese momento. Esto permite encontrar al ganador, porque la regla de prioridad es la misma en todo el conjunto: menor número de urgencia y, en empate, llegada más temprana.

Su complejidad temporal es **O(n)**, porque realiza un recorrido y compara cada paciente con el candidato actual.

## 4. Verificación de resultados

Ambos algoritmos reciben exactamente los mismos datos. Se verifica que devuelvan al mismo paciente prioritario, según urgencia y, en caso de empate, la llegada más temprana.

La verificación se realiza antes de registrar los tiempos, para asegurar que se comparan dos soluciones correctas del mismo problema.

**Resultado de verificación:** Pendiente de ejecutar el código.

## 5. Protocolo de medición

Se utilizarán cuatro tamaños de datos que se duplican:

- 1.000 pacientes
- 2.000 pacientes
- 4.000 pacientes
- 8.000 pacientes

Para cada tamaño:
1. Se ejecuta una prueba de calentamiento, que no se registra.
2. Se realizan tres mediciones de cada algoritmo.
3. Se calcula el promedio de las tres mediciones.
4. Se registran los tiempos en milisegundos.
5. Se calcula el factor de aumento respecto a la fila anterior dividiendo el promedio actual entre el promedio anterior.

Las pruebas se realizan en la misma computadora y con los mismos datos para ambos algoritmos, procurando no ejecutar otras tareas pesadas durante las mediciones.

## 6. Tabla de resultados

Completar con los tiempos reales del programa. No inventar valores.

| Pacientes | Fuerza bruta (ms, promedio) | Factor respecto a anterior | Voraz (ms, promedio) | Factor respecto a anterior |
|---:|---:|---:|---:|---:|
| 1.000 | Pendiente | — | Pendiente | — |
| 2.000 | Pendiente | Pendiente | Pendiente | Pendiente |
| 4.000 | Pendiente | Pendiente | Pendiente | Pendiente |
| 8.000 | Pendiente | Pendiente | Pendiente | Pendiente |

## 7. Conclusión

Completar después de medir:
- Indicar cuál algoritmo tardó menos y cuándo se notó más la diferencia.
- Explicar cómo cambió el tiempo al duplicar los pacientes.
- Comparar lo observado con el crecimiento esperado de O(n²) para fuerza bruta y O(n) para el voraz.
- Mencionar honestamente el ruido de las mediciones (estado del equipo, procesos en segundo plano y variaciones de ejecución). Por eso se calentó el programa, se repitió tres veces y se promedió.
