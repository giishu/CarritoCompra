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
        public Carrito carrito;

        public Tienda()
        {
            Categorias = new List<Categoria>();
            Productos = new List<Producto>();
            carrito = new Carrito();
            Inicializar();
        }

        public void inicializar()
        {
            // completar con categorias y productos
        }

        public void MostrarCategorias()
        {
            Console.WriteLine("\n=== CATEGORÍAS DISPONIBLES ===");
            for(int i = 0; i < Categorias.Count; i++)
            {
                Console.WriteLine($"{i+1}. {Categorias[i]}")
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
            Console.Write("Ingrese el codigo del producto: ");
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
            Console.Write("Ingrese la cantidad a eliminar: ");
            if(int.TryParse(Console.ReadLine(), out int codigo))
            {
                carrito.EliminarProducto(codigo);
            }
            else
            {
                Console.WriteLine("El código es inválido");
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

        public void MostarMenu()
        {
            // completar menu principal. aca va el switch
        }

    }
}
