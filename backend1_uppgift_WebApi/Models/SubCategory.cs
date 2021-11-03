using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend1_uppgift_WebApi.Models
{
    [Index(nameof(SubCategoryName), IsUnique = true)]
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string SubCategoryName { get; set; }

        // Kopplar till en category
        public int CategoryId { get; set; }

        // Får upp informationen om kategorin
        // Refererar till  Category
        public virtual Category Category { get; set; }

        // Få upp alla produkter under SubCategory
        //Koppling till Product. Skapar främmande nyckel relationen till Product
        // Har en lista av olika produkter i sig
        public virtual ICollection<Product> Products {get; set; }
}
}