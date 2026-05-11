using DoubleList;

// Lista de strings (puedes cambiar a int, double, etc.)
var list = new DoubleLinkedList<string>();
var option = string.Empty;

do
{
    option = Menu();
    switch (option)
    {
        // ── 1. ADICIONAR ────────────────────────────────────────
        case "1":
            Console.Write("Ingrese un valor: ");
            var input = Console.ReadLine() ?? string.Empty;
            if (input != string.Empty)
            {
                list.InsertOrdered(input);
                Console.WriteLine($"✔ '{input}' agregado.");
                Console.WriteLine($"  Lista: {list.ToStringForward()}");
            }
            break;

        // ── 2. MOSTRAR HACIA ADELANTE ───────────────────────────
        case "2":
            Console.WriteLine($"\nAdelante: {list.ToStringForward()}");
            break;

        // ── 3. MOSTRAR HACIA ATRÁS ──────────────────────────────
        case "3":
            Console.WriteLine($"\nAtrás:    {list.ToStringBackward()}");
            break;

        // ── 4. ORDENAR DESCENDENTEMENTE ─────────────────────────
        case "4":
            list.SortDescending();
            Console.WriteLine("✔ Lista ordenada descendentemente.");
            Console.WriteLine($"  Lista: {list.ToStringForward()}");
            break;

        // ── 5. MOSTRAR MODA(S) ──────────────────────────────────
        case "5":
            var modes = list.GetModes();
            if (modes.Count == 0)
                Console.WriteLine("No hay moda (todos los elementos son únicos o la lista está vacía).");
            else if (modes.Count == 1)
                Console.WriteLine($"Moda: {modes[0]}");
            else
                Console.WriteLine($"Modas: {string.Join(", ", modes)}");
            break;

        // ── 6. MOSTRAR GRÁFICO ──────────────────────────────────
        case "6":
            Console.WriteLine("\nGráfico de ocurrencias:");
            Console.WriteLine(list.GetChart());
            break;

        // ── 7. EXISTE ───────────────────────────────────────────
        case "7":
            Console.Write("Ingrese el valor a buscar: ");
            var searchVal = Console.ReadLine() ?? string.Empty;
            if (list.Exists(searchVal))
                Console.WriteLine($"✔ '{searchVal}' SÍ existe en la lista.");
            else
                Console.WriteLine($"✘ '{searchVal}' NO existe en la lista.");
            break;

        // ── 8. ELIMINAR UNA OCURRENCIA ──────────────────────────
        case "8":
            Console.Write("Ingrese el valor a eliminar (primera ocurrencia): ");
            var removeOne = Console.ReadLine() ?? string.Empty;
            bool removed = list.RemoveFirst(removeOne);
            Console.WriteLine(removed
                ? $"✔ Primera ocurrencia de '{removeOne}' eliminada."
                : $"✘ '{removeOne}' no se encontró en la lista.");
            Console.WriteLine($"  Lista: {list.ToStringForward()}");
            break;

        // ── 9. ELIMINAR TODAS LAS OCURRENCIAS ───────────────────
        case "9":
            Console.Write("Ingrese el valor a eliminar (todas las ocurrencias): ");
            var removeAll = Console.ReadLine() ?? string.Empty;
            int count = list.RemoveAll(removeAll);
            Console.WriteLine(count > 0
                ? $"✔ {count} ocurrencia(s) de '{removeAll}' eliminadas."
                : $"✘ '{removeAll}' no se encontró en la lista.");
            Console.WriteLine($"  Lista: {list.ToStringForward()}");
            break;

        // ── 0. SALIR ────────────────────────────────────────────
        case "0":
            Console.WriteLine("¡Hasta luego!");
            break;

        default:
            Console.WriteLine("✘ Opción inválida. Intente de nuevo.");
            break;
    }

} while (option != "0");


// ── MENÚ ────────────────────────────────────────────────────────
string Menu()
{
    Console.WriteLine();
    Console.WriteLine("╔══════════════════════════════════════════╗");
    Console.WriteLine("║       LISTA DOBLEMENTE LIGADA            ║");
    Console.WriteLine("╠══════════════════════════════════════════╣");
    Console.WriteLine("║  1. Adicionar                            ║");
    Console.WriteLine("║  2. Mostrar hacia adelante               ║");
    Console.WriteLine("║  3. Mostrar hacia atrás                  ║");
    Console.WriteLine("║  4. Ordenar descendentemente             ║");
    Console.WriteLine("║  5. Mostrar la(s) moda(s)                ║");
    Console.WriteLine("║  6. Mostrar gráfico                      ║");
    Console.WriteLine("║  7. Existe                               ║");
    Console.WriteLine("║  8. Eliminar una ocurrencia              ║");
    Console.WriteLine("║  9. Eliminar todas las ocurrencias       ║");
    Console.WriteLine("║  0. Salir                                ║");
    Console.WriteLine("╚══════════════════════════════════════════╝");
    Console.Write("Seleccione una opción: ");
    return Console.ReadLine() ?? string.Empty;
}
