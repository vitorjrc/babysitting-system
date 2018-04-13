using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuguDadah.Data {

    public class Professional {

        [Key]
        public string userName { get; set; }

        public string password { get; set; }

        public string eMail { get; set; }

        public string contact { get; set; }

        public double rating { get; set; }

        [Column(TypeName = "char(1)")]
        public string shift { get; set; }
    }
}
