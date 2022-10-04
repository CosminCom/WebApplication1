using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    // Clasa care se ocupa de conexiune
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            conn.Open();

            string statement = "SELECT ProductID,ProductName,Quantity FROM Products";

            SqlCommand cmd = new SqlCommand(statement, conn);
            List<Product> _product_lst = new List<Product>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    _product_lst.Add(product);
                }
            }

            conn.Close();
            return _product_lst;
        }
    }
}
