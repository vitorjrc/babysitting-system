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

        [Column(TypeName = "decimal(2, 1)")]
        [Required]
        [Display(Name = "Avaliação")]
        public float Rating { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Pagamento")]
        public string Payment { get; set; }

        [Required]
        [Display(Name = "Duração")]
        public int Duration { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        [Required]
        [Display(Name = "Preço")]
        public float Cost { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Estado")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "Observações")]
        public string Observations { get; set; }

        [Display(Name = "Cliente")]
        public Client Client { get; set; }

        [Display(Name = "Profissional")]
        public Professional Professional { get; set; }

    }
}
