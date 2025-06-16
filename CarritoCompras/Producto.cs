using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    class Producto
    {
        private static int contador = 1;

        public int Codigo { get; private set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Categoria Categoria { get; set; }

        public Producto(string nombre, decimal precio, int stock, Categoria categoria)
        {
            Codigo = contador++;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
        }

        public override string ToString()
        {
            return $"{Codigo} - {Nombre} - {Precio:F2} - Stock: {Stock} - Categoria: {Categoria.Nombre}";
        }
    }
}
