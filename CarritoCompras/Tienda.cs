using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    class Tienda
    {
        public List<Producto> Productos { get; private set; }
        public List<Categoria> Categorias { get; private set; }
        private Carrito carrito;

        public Tienda()
        {
            Categorias = new List<Categoria>();
            Productos = new List<Producto>();
            carrito = new Carrito();
            Inicializar();
        }

        private void Inicializar()
        {
            var electronica = new Categoria("Electrónica", "Dispositivos electrónicos y gadgets");
            var ropa = new Categoria("Ropa", "Prendas de vestir para hombres y mujeres");
            var alimentos = new Categoria("Alimentos", "Productos alimenticios y bebidas");

            Categorias.AddRange(new[] { electronica, ropa, alimentos });

            Productos.AddRange(new[]
            {
        new Producto("Samsung J5", 899.99m, 10, electronica),
        new Producto("Laptop para programadores", 1299.99m, 5, electronica),
        new Producto("Ipad pro", 199.50m, 8, electronica),
        new Producto("Remera con estamapdo de Los enanitos verdes", 24.99m, 20, ropa),
        new Producto("Jeans oversize fashion", 59.99m, 15, ropa),
        new Producto("Zapatillas ardidas", 39.99m, 10, ropa),
        new Producto("Agua mineral 1,5L", 9.99m, 15, alimentos),
        new Producto("Barrita kinder", 2.50m, 30, alimentos)
    });

        }

        public void MostrarCategorias()
        {
            Console.WriteLine("\n=== CATEGORÍAS DISPONIBLES ===");
            for(int i = 0; i < Categorias.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Categorias[i]}");
            }
        }

        public void MostrarProductos()
        {
            Console.WriteLine("\n=== PRODUCTOS DISPONIBLES ===");
            foreach (Producto producto in Productos)
            {
                Console.WriteLine(producto);
            }
        }

        public void MostrarProductosPorCategoria()
        {
            Console.WriteLine("\nSeleccione una categría:");
            MostrarCategorias();

            if(int.TryParse(Console.ReadLine(), out int opcion) && opcion >= 1 && opcion <= Categorias.Count)
            {
                Categoria categoriaSeleccionada = Categorias[opcion - 1];
                List<Producto> productosCategoria = new List<Producto>();
                foreach(Producto producto in Productos)
                {
                    if(producto.Categoria == categoriaSeleccionada)
                    {
                        productosCategoria.Add(producto);
                    }
                }

                Console.WriteLine($"\n === PRODUCTOS DE {categoriaSeleccionada.Nombre} ===");
                foreach(Producto producto in productosCategoria)
                {
                    Console.WriteLine(producto);
                }
            }
            else
            {
                Console.WriteLine("La opción es inválida");
            }
        }

        public void AgregarProductoAlCarrito()
        {
            MostrarProductos();
            Console.Write("\nIngrese el codigo del producto: ");
            if(int.TryParse(Console.ReadLine(), out int codigo))
            {
                Producto productoEncontrado = null;
                foreach(Producto producto in Productos)
                {
                    if(producto.Codigo == codigo)
                    {
                        productoEncontrado = producto;
                        break;
                    }
                }

                if(productoEncontrado != null)
                {
                    Console.Write("Ingrese la cantidad a agregar: ");
                    if(int.TryParse(Console.ReadLine(), out int cantidad))
                    {
                        carrito.AgregarProducto(productoEncontrado, cantidad);
                    }
                    else
                    {
                        Console.WriteLine("Cantidad inválida");
                    }
                }
                else
                {
                    Console.WriteLine("Codigo de producto invalido");
                }
            }
            else
            {
                Console.WriteLine("Codigo invalido");
            }
        }

        public void EliminarProductoCarrito()
        {
            if(carrito.Items.Count == 0)
            {
                Console.WriteLine("El carrito está vacío, no se puede eliminar ningún producto");
                return;
            }

            carrito.Contenido();

            Console.Write("\nIngrese el código del producto a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int codigoProducto))
            {
                Console.WriteLine("Código inválido.");
                return;
            }

            Console.Write("Ingrese la cantidad a eliminar (0 o negativo para eliminar todo): ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Error: Cantidad inválida. Se eliminará el producto completo.");
            }

            bool resultado = carrito.EliminarProducto(codigoProducto, cantidad);

            if (!resultado)
            {
                Console.WriteLine("No se pudo completar la operación. Verifique el código del producto.");
            }
            else
            {
                Console.WriteLine("\nCarrito actualizado: ");
                carrito.Contenido();
            }
        }

        public void FinalizarCompra()
        {
            if(carrito.Items.Count == 0)
            {
                Console.WriteLine("El carrito está vacío, no se puede finalizar la compra");
                return;
            }
            Console.WriteLine("\n=== RESUMEN DE COMPRA ===");
            carrito.Contenido();

            Console.Write("\n ¿Desea confirmar la compra? (s/n)");
            string confirmacion = Console.ReadLine();
            if(confirmacion != null)
            {
                confirmacion = confirmacion.ToLower();
            }

            if(confirmacion == "s" || confirmacion == "si")
            {
                foreach(ItemCarrito item in carrito.Items)
                {
                    item.Producto.Stock -= item.Cantidad;
                }
                Console.WriteLine("\nCompra realizada con éxito");
                Console.WriteLine($"Total pagado: ${carrito.CalcularTotal():F2}");

                carrito.Vaciar();
            }
            else
            {
                Console.WriteLine("Compra cancelada");
            }
        }

        public void MostrarMenu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Ver categorías disponibles");
                Console.WriteLine("2. Ver todos los productos");
                Console.WriteLine("3. Ver productos por categoría");
                Console.WriteLine("4. Agregar producto al carrito");
                Console.WriteLine("5. Eliminar producto del carrito");
                Console.WriteLine("6. Ver contenido del carrito");
                Console.WriteLine("7. Ver total a pagar");
                Console.WriteLine("8. Finalizar compra");
                Console.WriteLine("9. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MostrarCategorias();
                        break;

                    case "2":
                        MostrarProductos();
                        break;

                    case "3":
                        MostrarProductosPorCategoria();
                        break;

                    case "4":
                        AgregarProductoAlCarrito();
                        break;

                    case "5":
                        EliminarProductoCarrito();
                        break;

                    case "6":
                        carrito.Contenido();
                        break;

                    case "7":
                        Console.WriteLine($"\nTOTAL A PAGAR (con IVA): {carrito.CalcularTotal():C2}");
                        break;

                    case "8":
                        FinalizarCompra();
                        break;

                    case "9":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("\nOpción no válida. Por favor ingrese un número del 1 al 9.");
                        break;
                }

                if (!salir && opcion != "9")
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar");
                    Console.ReadKey();
                }
            }
        }

    }
}
