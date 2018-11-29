using System;
using System.Collections.Generic;
using System.Text;

namespace BoxObjects
{
    public class Reservations
    {
        public int reservation_id { get; set; }
        public int fk_reservation_week { get; set; }
        public int fk_reservation_user { get; set; }
        public int  reservation_amount { get; set; }
        public int fk_reservation_item { get; set; }

    }
}
