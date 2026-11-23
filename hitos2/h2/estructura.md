# Estructuras elegidas – Kardex médico SEPSA
// luis alberto mamani lopez
## Estructuras comparadas
- **Ingenua: `List<int>`**. Guarda las claves de pacientes sin mantener orden. Para encontrar una clave se recorre la lista. Para consultas de orden, se recorre o se ordena una copia.
- **Adecuada: `SortedSet<int>`**. Guarda claves únicas ordenadas. Mantiene el orden al insertar o eliminar y facilita consultas ordenadas y por rango.

Se compara solo la estructura en memoria; no se incluye SQL Server, Laravel ni tiempo de pantalla.

## Costos Big-O

| Operación | `List<int>` ingenua | `SortedSet<int>` | Motivo |
|---|---:|---:|---|
| Buscar por clave | O(n) | O(log n) | Lista recorre elementos; árbol ordenado busca por comparaciones. |
| Insertar | O(n) | O(log n) | En la lista se comprueba que no exista; el árbol ubica la clave. |
| Borrar por clave | O(n) | O(log n) | La lista busca y elimina desplazando elementos; el árbol busca y elimina. |
| Menor | O(n) | O(1) aprox. | Lista revisa todos; el árbol mantiene el mínimo accesible. |
| Siguiente clave | O(n) | O(log n) aprox. | Lista revisa candidatos; árbol puede buscar el primer elemento mayor. |
| Rango | O(n) | O(log n + k) | Lista revisa todo; árbol localiza el inicio y enumera k resultados. |
| Recorrer en orden | O(n log n) | O(n) | Lista necesita ordenar una copia; el árbol ya se recorre ordenado. |

## ¿Por qué `SortedSet`?
El Kardex consulta pacientes por una clave, y también puede necesitar listarlos ordenadamente o obtener un rango. `SortedSet` es apropiado para este escenario porque mantiene las claves ordenadas y evita ordenar toda la colección cada vez que se requiere un recorrido. Sus búsquedas, inserciones y eliminaciones son O(log n), en lugar de recorrer toda la lista.

En una aplicación real, los datos permanentes siguen en SQL Server. Esta comparación es un ejercicio de estructuras de datos, no una propuesta para reemplazar la base de datos.

