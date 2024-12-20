using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Entities
{
    public class TaxiTrip
    {
        public int TaxiTripId { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime DropoffDateTime { get; set; }
        public int? PassengerCount { get; set; }
        public decimal TripDistance { get; set; }
        public string? StoreAndFwdFlag { get; set; }
        public int PULocationId { get; set; }
        public int DOLocationId { get; set; }
        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set; }
    }
}
