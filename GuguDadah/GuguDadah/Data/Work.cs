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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Avaliação")]
        public float? Rating { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Tipo")]
        [StringLength(1)]
        public string Type { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Pagamento")]
        [StringLength(1)]
        public string Payment { get; set; }

        [Required]
        [Display(Name = "Duração")]
        public int? Duration { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        [Required]
        [Display(Name = "Preço")]
        public decimal? Cost { get; set; }

        [Column(TypeName = "char(1)")]
        [Required]
        [Display(Name = "Estado")]
        [StringLength(1)]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        [Display(Name = "Morada")]
        [StringLength(100)]
        public string Address { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "Observações")]
        public string Observations { get; set; }

        [Display(Name = "Cliente")]
        [Required]
        public Client Client { get; set; }

        [Display(Name = "Profissional")]
        [Required]
        public Professional Professional { get; set; }

    }
}
