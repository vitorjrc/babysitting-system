using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuguDadah.Data {

    public class Client {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string eMail { get; set; }

        public string contact { get; set; }

        public byte[] avatar { get; set; }

    }
}
