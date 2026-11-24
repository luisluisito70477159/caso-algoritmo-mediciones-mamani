
# H3 – Estrategia del caso Kardex SEPSA

## 1. Mi caso

Mi proyecto es un Kardex de pacientes del consultorio médico de SEPSA. Permite organizar los registros y realizar operaciones como buscar, insertar, eliminar y consultar pacientes usando una clave.

Estas operaciones principales son de administración de datos. No necesariamente requieren una estrategia voraz o programación dinámica.

## 2. Problema relacionado que usaré en el H3

Para aplicar y comparar una estrategia, planteo un problema relacionado con la atención de pacientes: seleccionar al paciente que debe ser atendido primero según su urgencia y hora de llegada.

Las reglas son:
- La urgencia 1 tiene mayor prioridad que la urgencia 2, 3, 4 o 5.
- Si dos pacientes tienen la misma urgencia, se elige al que llegó primero.

Este ejemplo es una regla de selección para el ejercicio de algoritmos; no reemplaza el criterio médico real de triage.

## 3. ¿Por qué se puede aplicar una estrategia voraz?

La estrategia voraz consiste en conservar en cada paso al mejor candidato encontrado hasta ese momento. Al revisar un paciente nuevo, lo comparo con el candidato actual. Si el nuevo tiene mayor prioridad (urgencia más alta o, en caso de empate, llegó antes), lo elijo como candidato. Si no, mantengo al anterior.

La elección local es segura porque, según la regla definida, un paciente que tiene prioridad sobre el candidato actual también tiene prioridad sobre los pacientes que ya fueron descartados. Por eso, al terminar de revisar la lista, el candidato que queda es el paciente que debe ser seleccionado primero.

## 4. ¿Qué problema más pequeño queda?

Después de seleccionar al paciente prioritario, quedan los demás pacientes esperando. Se puede aplicar la misma regla al grupo restante para encontrar al siguiente prioritario.

Por tanto, la selección se repite sobre un conjunto más pequeño, manteniendo la misma regla de prioridad.

## 5. ¿Por qué no uso programación dinámica?

No aplico programación dinámica porque este problema no necesita resolver subproblemas repetidos ni guardar sus resultados para reutilizarlos. Basta con recorrer los pacientes y mantener al mejor candidato encontrado. La programación dinámica sería innecesaria para esta selección.

## 6. Conclusión

Las operaciones principales del Kardex SEPSA se enfocan en administrar registros y no requieren directamente una estrategia voraz o programación dinámica. Para el H3 usaré el problema relacionado de selección del paciente prioritario. En este caso, la estrategia voraz es apropiada porque la mejor elección local —seleccionar al paciente con mayor prioridad según las reglas— también es la elección correcta para obtener al paciente prioritario del conjunto completo. El grupo restante conserva el mismo problema de selección.
