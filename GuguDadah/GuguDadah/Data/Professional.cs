using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuguDadah.Data {

    public class Professional {

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
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Turno: M, T ou N")]
        [StringLength(1)]
        public string Shift { get; set; }

        [Required]
        public float? Rating { get; set; }

        [Required]
        [Display(Name = "Contacto telefónico")]
        public int? Contact { get; set; }

        [Required]
        public byte[] Avatar { get; set; }

        [Required]
        [Display(Name = "Data de registo")]
        public DateTime RegistrationDate { get; set; }

        [Column(TypeName = "text")] 
        [Display(Name = "Apresentação")]
        public string Presentation { get; set; }
    }
}
