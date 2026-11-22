// luis alberto mamani lopez
//P2.1 - Elección de estructuras de datos
// ¿Por qué no conviene utilizar una lista simple?

Una lista simple debe revisar a todos los pacientes uno por uno para encontrar al que tiene mayor urgencia. Cuando hay 100.000 pacientes, este proceso puede llegar a realizar casi 5.000 millones de comparaciones, haciendo que la búsqueda sea muy lenta.

Por eso se elige una cola de prioridad para atender pacientes y un HashSet para consultar si un paciente sigue esperando.
//¿Cuándo cambiaría mi elección?

Cambiaría la estructura de datos si aumentara considerablemente la cantidad de pacientes o si cambiaran las operaciones que realiza el sistema.
