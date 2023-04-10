﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Atendimento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int? ID { get; set; }

        [Required(ErrorMessage = "Garçom é obrigatório")]
        public Garcom? Garcom { get; set; }

        [Required(ErrorMessage = "Mesa é obrigatória")]
        public Mesa? Mesa { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? InitDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool? Status { get; set; }
    }
}
