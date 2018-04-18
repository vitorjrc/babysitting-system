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
        [StringLength(20)]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        [Display(Name = "Nome")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        [Display(Name = "Password")]
        [StringLength(50)]
        public string Password { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Estatuto")]
        [StringLength(1)]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Contacto telefónico")]
        public int? Contact { get; set; }

        [Required]
        public byte[] Avatar { get; set; }

    }
}
