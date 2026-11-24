# H3 – Estrategia del caso Kardex SEPSA

## Mi caso

Mi proyecto consiste en un Kardex de pacientes del consultorio médico de SEPSA, donde se pueden registrar pacientes y realizar operaciones como buscar, insertar, eliminar y consultar registros.

## ¿Se puede aplicar una estrategia voraz?

En las operaciones principales de mi Kardex no es necesario utilizar una estrategia voraz, porque estas operaciones se enfocan en organizar, buscar y administrar los registros de pacientes, y no en encontrar una solución óptima entre varias opciones.

Sin embargo, puedo aplicar una estrategia voraz a un problema relacionado con mi caso: seleccionar al paciente que debe ser atendido primero según su urgencia y hora de llegada.

## Estrategia voraz propuesta

La estrategia consiste en seleccionar en cada paso al paciente con mayor prioridad. Se considera primero la urgencia más alta y, si dos pacientes tienen la misma urgencia, se elige al que llegó primero.

Esta elección permite obtener directamente al paciente que corresponde atender primero, sin necesidad de revisar todas las posibles ordenaciones de pacientes.

## ¿Por qué funciona?

La elección local consiste en seleccionar al paciente más urgente y, en caso de empate, al que llegó antes. Esta elección es la correcta para el primer paciente atendido, ya que ningún otro paciente tiene una prioridad superior.

Después de seleccionar al paciente, queda un grupo más pequeño de pacientes pendientes, al cual se puede aplicar nuevamente la misma regla.

## ¿Se aplica programación dinámica?

No es necesario aplicar programación dinámica a este problema, porque no se presentan subproblemas repetidos cuyos resultados debamos guardar y reutilizar. La selección se puede realizar mediante una comparación directa de los pacientes.

## Conclusión

Mi Kardex no necesita directamente programación dinámica ni estrategia voraz para sus operaciones principales. Para el H3 utilizaré un problema relacionado con la atención de pacientes, donde una estrategia voraz permite encontrar al paciente prioritario de manera más eficiente que una solución por fuerza bruta.
