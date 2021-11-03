using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend1_uppgift_WebApi.Models
{
    // Detta vill jag ha när jag skapar en SubCategory
    public class CreateSubCategoryModel
    {
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string SubCategoryName { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
