using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
    public class MovieModel
    {
        public int MovieID { get; set; }
        public string MovieImage { get; set; }
        public string Country { get; set; }
        public string MovieName { get; set; }
        public string Description { get; set; }
        public string ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Producers{ get; set; }
        public string GenreName { get; set; }
        public string Duration { get; set; }
        public string Mark { get; set; }
        public double Price { get; set; }
    }
}
