using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuguDadah.Data
{
    public class Work
    {

        [Key]
        public int ID { get; set; }

        [Column(TypeName = "char(1)")]
        public string type { get; set; }

        public int duration { get; set; }

        public float cost { get; set; }

        public DateTime date { get; set; }

        public float rating { get; set; }

        [Column(TypeName = "char(1)")]
        public string payment { get; set; }

        [Column(TypeName = "char(1)")]
        public string status { get; set; }

        public string observations { get; set; }

        public string address { get; set; }

        public Client client { get; set; }

        public Professional professional { get; set; }

    }
}
