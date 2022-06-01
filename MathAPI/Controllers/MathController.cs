using MathAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MathAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {
        private readonly ILogger<MathController> logger;

        public MathController(ILogger<MathController> _logger)
        {
            logger = _logger;
        }

        [HttpGet]
        public List<Customer> Get(string CustomerId)
        {
            SqlConnection sqlConnection = new SqlConnection(@"data source=(localdb)\mssqllocaldb;initial catalog=Northwind;integrated security=True");


            SqlCommand sqlCommand = new SqlCommand($"Select CustomerId,CompanyName,Address from Customers where CustomerId = '{CustomerId}'");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlConnection.Open();




            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Customer> dto = new List<Customer>();
            while (reader.Read())
            {
                Customer dtoItem = new Customer();
                dtoItem.CustomerId = reader[0].ToString();
                dtoItem.CompanyName = reader[1].ToString();
                //dtoItem.Adress = reader[2].ToString();
                dto.Add(dtoItem);
            }
            sqlConnection.Close();
            return dto;
        }
    }
}
