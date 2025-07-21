using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ProductInventory_ADO.Models
{
    public class ProductRepository
    {
        private string connectionString;

        public ProductRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM Products", con);
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"]
                    });
                }
            }
            return products;
        }

        public Product GetById(int id)
        {
            Product product = null;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM Products WHERE ProductId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"]
                    };
                }
            }
            return product;
        }

        public void Add(Product product)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO Products (Name, Description, Price) VALUES (@Name, @Description, @Price)", con);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description ?? "");
                cmd.Parameters.AddWithValue("@Price", product.Price);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product product)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("UPDATE Products SET Name = @Name, Description = @Description, Price = @Price WHERE ProductId = @ProductId", con);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description ?? "");
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM Products WHERE ProductId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
