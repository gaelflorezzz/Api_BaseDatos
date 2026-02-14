namespace Api_BaseDatos.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

namespace Api_BaseDatos.Models
{
    public class ProductoResponse
    {
        public required List<Producto> Results { get; set; }
    }
}
