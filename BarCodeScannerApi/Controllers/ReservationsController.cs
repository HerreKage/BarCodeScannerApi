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
    public class ReservationsController : ControllerBase
    {
        // GET: api/Reservations

        // connection string hentes fra azure
        private static string connstring = 


        // GET: api/BarCodeDatabase
        [HttpGet(Name ="GetAllReservations")]
        public IEnumerable<Reservations> Get()
        {
            SqlConnection conn = new SqlConnection(connstring);
            //ændre så den passer med vores
            SqlCommand query = new SqlCommand("SELECT * FROM  reservations", conn);
            conn.Open();
            SqlDataReader laeser = query.ExecuteReader();
            //set objekt til hvad vores objekt er

          List<Reservations> reservationsList=new List<Reservations>();

            if (laeser.HasRows)
            {
                while (laeser.Read())
                {


                    Reservations hs=new Reservations{reservation_id = laeser.GetInt32(0), fk_reservation_week = laeser.GetInt32(1),fk_reservation_user = laeser.GetInt32(2), reservation_amount = laeser.GetInt32(3),fk_reservation_item = laeser.GetInt32(4) };
                    reservationsList.Add(hs);

                }

            }

            return reservationsList;
        }


        // GET: api/reservations/1
        [HttpGet("{id}", Name = "GetOneReservation")]
        public List<Reservations> GetOne(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"SELECT * FROM reservations WHERE reservation_id={id}", conn);
            conn.Open();
            SqlDataReader laeser = query.ExecuteReader();
            List<Reservations> rl = new List<Reservations>();
            //set objekt til hvad vores objekt er

            if (laeser.HasRows)
            {
                while (laeser.Read())
                {
                    Reservations hs = new Reservations { reservation_id = laeser.GetInt32(0), fk_reservation_week = laeser.GetInt32(1), fk_reservation_user = laeser.GetInt32(2), reservation_amount = laeser.GetInt32(3), fk_reservation_item = laeser.GetInt32(4) };
                    rl.Add(hs);
                }
            }
            return rl;
        }







        // POST: api/Reservations
        [HttpPost]

        public void Post([FromBody] Reservations value)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"INSERT INTO reservations(fk_reservation_week, fk_reservation_user, fk_reservation_item,reservation_amount) VALUES({value.fk_reservation_week}, {value.fk_reservation_user},{value.fk_reservation_item},{value.reservation_amount})", conn);
            conn.Open();
            query.ExecuteNonQuery();

        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Reservations value)
        {

            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"UPDATE reservations SET fk_reservation_week={value.fk_reservation_week}, fk_reservation_user= {value.fk_reservation_user}, fk_reservation_item={value.fk_reservation_item}, reservation_amount={value.reservation_amount} WHERE reservation_id = {id}", conn);
            conn.Open();
            query.ExecuteNonQuery();
        }




        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand query = new SqlCommand($"DELETE FROM reservations WHERE reservation_id = {id}", conn);
            conn.Open();
            query.ExecuteNonQuery();

        }
    }
}
