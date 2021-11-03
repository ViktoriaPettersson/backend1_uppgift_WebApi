using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend1_uppgift_WebApi.Models
{
    // Modifierad version av Category
    // Det jag vill ha med när jag skapar en Category
    public class CreateCategoryModel
    {
        // Vill bara använda en CategoryName när jag skapar en kategori
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string CategoryName { get; set; }
    }
}
