using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend1_uppgift_WebApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        // Är ej unik, det baseras på Id:
        [Required]
        [Column(TypeName = "nvarchar(200")]
        public string  Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200")]
        public string ShortDescription { get; set; }

        // Kan vara fristående
        [Column(TypeName = "nvarchar(max")]
        public string LongDescription { get; set; }

        public decimal Price { get; set; }

    }
}
