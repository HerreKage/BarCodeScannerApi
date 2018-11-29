using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BoxObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeScannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeksController : ControllerBase
    {
        private static string connstring = 

        // GET: api/BarCodeDatabase
        [HttpGet(Name = "GetAllWeeks")]
        public IEnumerable<Weeks> Get()
        {
            SqlConnection conn = new SqlConnection(connstring);
            //ændre så den passer med vores
            SqlCommand query = new SqlCommand("SELECT * FROM  weeks", conn);
            conn.Open();
            SqlDataReader laeser = query.ExecuteReader();
            //set objekt til hvad vores objekt er

           List<Weeks> weeksList=new List<Weeks>();

            if (laeser.HasRows)
            {
                while (laeser.Read())
                {


                    Weeks hs = new Weeks {week_id=laeser.GetInt32(0),week_week = laeser.GetInt32(1)};
                    weeksList.Add(hs);

                }

            }

            return weeksList;
        }


        [HttpGet("{id}", Name = "GetOneUser")]
        public List<Weeks> GetOne(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"SELECT * FROM  weeks WHERE week_id={id}", conn);
            conn.Open();
            SqlDataReader reader = query.ExecuteReader();
            List<Weeks> ws = new List<Weeks>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Weeks hs = new Weeks { week_id = reader.GetInt32(0), week_week = reader.GetInt32(1) };
                    ws.Add(hs);


                }

            }

            return ws;

        }

        // POST: api/Reservations
        [HttpPost]

        public void Post([FromBody] Weeks value)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"INSERT INTO weeks(week_week) VALUES({value.week_week})", conn);
            conn.Open();
            query.ExecuteNonQuery();

        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Weeks value)
        {

            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"UPDATE weeks SET week_week={value.week_week} WHERE week_id = {id}", conn);
            conn.Open();
            query.ExecuteNonQuery();
        }




        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"DELETE FROM weeks WHERE week_id = {id}", conn);
            conn.Open();
            query.ExecuteNonQuery();

        }
    }
}
