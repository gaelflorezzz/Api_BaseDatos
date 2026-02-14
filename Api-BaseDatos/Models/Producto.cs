using System.ComponentModel.DataAnnotations.Schema;

namespace Api_BaseDatos.Models
{
    public class Producto
    {
        public long Pro_Id { get; set; } 
        public string? Pro_Nombre { get; set; } = string.Empty;
        public long Pro_Stock { get; set; }
        public decimal Pro_Precio { get; set; }
        public string? Pro_Tipo { get; set; } = string.Empty;
    }
}
