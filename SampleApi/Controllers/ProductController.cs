using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SampleApi.Models;
using System.Collections.Immutable;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    { 

        private readonly string connectionstring;
        public ProductController(IConfiguration configuration)
        { 
        connectionstring = configuration [ "ConnectionStrings:sqlserverd" ] ?? "";
    
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            try
            {

                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "INSRT INTO PRODUCT" +
                        "(name, brand, category, price, discription ) VALUES " +
                        "(@name, @brand, @category, @price, @discription)";

                    using (var command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@name", productDto.Name);
                        command.Parameters.AddWithValue("@brand", productDto.price); 
                        command.Parameters.AddWithValue("@brand", productDto.Brand);
                        command.Parameters.AddWithValue("@brand", productDto.Category);
                        command.Parameters.AddWithValue("@brand", productDto.Discription);

                        command.ExecuteNonQuery();


                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "sorry, but we have exception");
                return BadRequest(ModelState);
            }
            return Ok();

        }
        [HttpGet]
        public IActionResult GetProduct() { 
            List<Product> products = new List<Product>();

            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "select * from products";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using(var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               Product product = new Product();

                                product.Id = reader.GetInt32(0);
                                product.Name= reader.GetString(1);
                                product.Description= reader.GetString(2);   
                                product.Category= reader.GetString(3);
                                product.Price = reader.GetString(4);
                                product.Brand = reader.GetString(5);
                                
                                products.Add(product);
                                
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "sorry, but we have exception");
                return BadRequest(ModelState);
            }
            return Ok(products);
            
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id , ProductDto productDto) {

            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string sql = "update produact set name name=@name , brand=@brand, " +
                           "category=@category, discription=@discription WHERE id=@id";

                    using (var command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@name", productDto.Name);
                        command.Parameters.AddWithValue("@brand", productDto.price);
                        command.Parameters.AddWithValue("@brand", productDto.Brand);
                        command.Parameters.AddWithValue("@brand", productDto.Category);
                        command.Parameters.AddWithValue("@brand", productDto.Discription);
                        command.Parameters.AddWithValue("id", id);

                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "sorry, but we have exception");
                return BadRequest(ModelState);

            }
            return Ok();
        }

    }
}
