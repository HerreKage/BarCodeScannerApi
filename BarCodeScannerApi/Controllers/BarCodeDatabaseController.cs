using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BoxObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.Extensions.Configuration;

namespace BarCodeScannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarCodeDatabaseController : ControllerBase
    {


        // connection string hentes fra azure
        private static string connstring = 


       // GET: api/BarCodeDatabase
       [HttpGet(Name = "GetAll")]
        public IEnumerable<Items> Get()
        {
            SqlConnection conn = new SqlConnection(connstring);
            //ændre så den passer med vores
             SqlCommand query = new SqlCommand("SELECT * FROM  items", conn);
            conn.Open();
            SqlDataReader laeser = query.ExecuteReader();
            //set objekt til hvad vores objekt er
            List<Items> itemsList = new List<Items>();
            //System.Diagnostics.Debug.WriteLine(laeser);

            if (laeser.HasRows)
            {
                while (laeser.Read())
                {
                   
                    Items hs=new Items{item_uid= laeser[0].ToString(),item_name = laeser[1].ToString(),item_amount = laeser.GetInt32(2)};
                    itemsList.Add(hs);
                   
                }

            }

            return itemsList;
        }

        // GET: api/BarCodeDatabase/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<Items> GetOne(string id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"SELECT * FROM  Items WHERE item_uid={id}", conn);
            conn.Open();
     


            SqlDataReader reader = query.ExecuteReader();
            reader.Read();

            List<Items> Li = new List<Items>();

            Items hs = new Items { item_uid = reader[0].ToString(), item_name = reader[1].ToString(), item_amount = reader.GetInt32(2) };
            Li.Add(hs);


            return Li;



        }


        //// POST: api/Reservations
        //[HttpPost]

        //public void Post([FromBody] Items value)
        //{
        //    SqlConnection conn = new SqlConnection(connstring);
        //    SqlCommand query = new SqlCommand($"INSERT INTO Items(item_uid, item_name, item_amount) VALUES({value.item_uid}, {value.item_name},{value.item_amount})", conn);
        //    conn.Open();
        //    query.ExecuteNonQuery();

        //}

        // PUT: api/Reservations/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] Items value)
        //{

        //    SqlConnection conn = new SqlConnection(connstring);
        //    SqlCommand query = new SqlCommand($"UPDATE Items SET items_uid={value.item_uid}, item_name= {value.item_name}, item_amount={value.item_amount}, WHERE item_uid = {id}", conn);
        //    conn.Open();
        //    query.ExecuteNonQuery();
        //}




        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    SqlConnection conn = new SqlConnection(connstring);
        //    SqlCommand query = new SqlCommand($"DELETE FROM items WHERE item_uid = {id}", conn);
        //    conn.Open();
        //    query.ExecuteNonQuery();

        //}
    }
}
