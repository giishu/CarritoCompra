using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    class ItemCarrito
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public ItemCarrito(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public decimal Subtotal()
        {
            decimal subtotal = Producto.Precio * Cantidad;

            if(Cantidad >= 5)
            {
                subtotal *= 0.85m;
            }
            return subtotal;
        }
        public override string ToString()
        {
            return $"{Producto.Codigo} - {Producto.Nombre} x{Cantidad} - ${Producto.Precio:F2} c/u (Total: ${(Cantidad * Producto.Precio):F2})";
        }
    }
}
