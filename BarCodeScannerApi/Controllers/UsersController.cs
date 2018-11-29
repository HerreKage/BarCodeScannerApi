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
    public class UsersController : ControllerBase
    {
        //GET: api/Users
        private static string connstring = 

        // GET: api/BarCodeDatabase
        [HttpGet(Name = "GetAllUsers")]
        public IEnumerable<Users> Get()
        {
            SqlConnection conn = new SqlConnection(connstring);
            //ændre så den passer med vores
            SqlCommand query = new SqlCommand("SELECT * FROM  users", conn);
            conn.Open();
            SqlDataReader laeser = query.ExecuteReader();
            //set objekt til hvad vores objekt er
            List<Users> UsersList=new List<Users>();

            if (laeser.HasRows)
            {
                while (laeser.Read())
                {


                    Users hs = new Users {user_id = laeser.GetInt32(0),user_category = laeser[1].ToString(), user_name = laeser[2].ToString(), user_admin = laeser.GetInt32(3)};
                    UsersList.Add(hs);

                }

            }

            return UsersList;
        }


        [HttpGet("{id}", Name = "GetOneUsers")]
        public List<Users> GetOne(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"SELECT * FROM  users WHERE user_id={id}", conn);
            conn.Open();
            SqlDataReader reader = query.ExecuteReader();
            List<Users> us=new List<Users>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Users hs = new Users { user_id = reader.GetInt32(0), user_category = reader[1].ToString(), user_name = reader[2].ToString(), user_admin = reader.GetInt32(3) };
                    us.Add(hs);
                }               
            }

            return us;
        }


        // POST: api/Reservations
        [HttpPost]

        public void Post([FromBody] Users value)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"INSERT INTO users(user_category, user_name, user_admin) VALUES('{value.user_category}', '{value.user_name}', {value.user_admin})", conn);
            conn.Open();
            query.ExecuteNonQuery();

        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Users value)
        {

            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"UPDATE users SET user_name='{value.user_name}', user_category= '{value.user_category}', user_admin={value.user_admin} WHERE user_id = {id}", conn);
            conn.Open();
            query.ExecuteNonQuery();
        }




        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"DELETE FROM users WHERE user_id = {id}", conn);
            conn.Open();
            query.ExecuteNonQuery();

        }
    }
}
