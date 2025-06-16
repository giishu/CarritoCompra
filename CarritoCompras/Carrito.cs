using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    class Carrito
    {
        public List<ItemCarrito> Items { get; private set; }

        public Carrito()
        {
            Items = new List<ItemCarrito>();  
        }

        public bool AgregarProducto(Producto producto, int cantidad)
        {
            if(cantidad <= 0)
            {
                Console.WriteLine("La cantidad del producto debe ser mayor que 0");
                return false;
            }

            ItemCarrito itemExistente = null;
            foreach(ItemCarrito item in Items)
            {
                if(item.Producto.Codigo == producto.Codigo)
                {
                    itemExistente = item;
                    break;
                }
            }

            int cantidadTotal = cantidad;
            if(itemExistente != null)
            {
                cantidadTotal = cantidad + itemExistente.Cantidad;
            }

            if (cantidadTotal > producto.Stock)
            {
                Console.WriteLine($"No hay stock suficiente del producto. Stock disponible: {producto.Stock}");
                return false;
            }

            if(itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
            }
            else
            {
                Items.Add(new ItemCarrito(producto, cantidad));
            }

            Console.WriteLine($"Producto agregado al carrito: {producto.Nombre} x{cantidad}");
            return true;
        }

        public bool EliminarProducto(int codigoProducto)
        {
            ItemCarrito itemAEliminar = null;
            foreach(ItemCarrito item in Items)
            {
                if(itemAEliminar.Producto.Codigo == codigoProducto)
                {
                    itemAEliminar = item;
                    break;
                }
            }

            if(itemAEliminar != null)
            {
                Items.Remove(itemAEliminar);
                Console.WriteLine($"Producto eliminado: {itemAEliminar.Producto.Nombre}");
                return true;
            }
            Console.WriteLine("No se encontro el producto a eliminar");
            return false;
        }

        public decimal CalcularSubtotal()
        {
            decimal total = 0;
            foreach(ItemCarrito item in Items)
            {
                total += item.Subtotal();
            }
            return total;
        }

        public decimal CalcularTotal()
        {
            decimal subtotal = CalcularSubtotal();
            return subtotal * 1.21m;
        }

        public void Contenido()
        {
            if(Items.Count == 0)
            {
                Console.WriteLine("El carrito esta vacio");
                return;
            }
            Console.WriteLine("\n === CARRITO ===");
            foreach(ItemCarrito item in Items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\nSubtotal: ${CalcularSubtotal():F2}");
            Console.WriteLine($"Total: ${CalcularTotal():F2}");
        }

        public void Vaciar()
        {
            Items.Clear();
        }
    }
}
