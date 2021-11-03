using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend1_uppgift_WebApi.Models
{
    public class CreateProductModel
    {
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string ShortDescription { get; set; }

        // Sätter ett default value på LongDescription eftersom den ej är required
        [Column(TypeName = "nvarchar(max)")]
        public string LongDescription { get; set; } = "";

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
    }
}
