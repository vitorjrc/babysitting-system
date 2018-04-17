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
        [Column(TypeName = "varchar(20)")]
        [Required]
        [Display(Name = "Nickname")]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Password { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Estatuto")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Contacto telefónico")]
        public int Contact { get; set; }

        [Required]
        public byte[] Avatar { get; set; }

    }
}
