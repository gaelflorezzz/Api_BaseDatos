using Api_BaseDatos.Models;
using Api_BaseDatos.Models.DB;
using Supabase;

namespace Api_BaseDatos.Services
{
    public class ProductosService
    {
        private readonly Client _supabase;
        private readonly HttpClient httpClient;
        private readonly string _url;
        private readonly string _apiKey;

        public ProductosService(Client supabase, HttpClient httpClient, IConfiguration configuration)
        {
            _supabase = supabase;
            this.httpClient = httpClient;
            _url = configuration["SB:ApiURL"] ;
            _apiKey = configuration["SB:ApiKey"];
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            var res = await _supabase.From<ProductoDb>().Get();

            return res.Models.Select(p => new Producto
            {
                Pro_Id = p.Pro_Id,
                Pro_Nombre = p.Pro_Nombre,
                Pro_Stock = p.Pro_Stock,
                Pro_Precio = p.Pro_Precio,
                Pro_Tipo = p.Pro_Tipo
            }).ToList();

        }

        public async Task Insertar(ProductoDb p)
        {
            Console.WriteLine("Insertando productos...");

            var db = new ProductoDb
            {
                Pro_Nombre = p.Pro_Nombre,
                Pro_Stock = p.Pro_Stock,
                Pro_Precio = p.Pro_Precio,
                Pro_Tipo = p.Pro_Tipo
            };

            var res = await _supabase.From<ProductoDb>().Insert(db);
        }

        public async  Task<bool> Actualizar(Producto p)
        {
            try
            {
                var db = new ProductoDb
                {
                    Pro_Id = p.Pro_Id,
                    Pro_Nombre = p.Pro_Nombre,
                    Pro_Stock = p.Pro_Stock,
                    Pro_Precio = p.Pro_Precio,
                    Pro_Tipo = p.Pro_Tipo
                };

                var resultado = await _supabase.From<ProductoDb>().Where(x => x.Pro_Id == p.Pro_Id).Update(db);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el producto: {ex.Message}");
                return false;
            }

            return true;
        }

        public async Task<Producto> ObtenerPorId(long id)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("apikey", _apiKey);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await httpClient.GetAsync($"{_url}/rest/v1/Prodcutos?Pro_Id=eq.{id}&select=*");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Producto>>(json);

                return lista.FirstOrDefault();
            }
        }
    }
}
