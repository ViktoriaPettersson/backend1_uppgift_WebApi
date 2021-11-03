using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend1_uppgift_WebApi.Models
{
    // Name ska vara unik
    [Index(nameof(CategoryName), IsUnique =true)]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string CategoryName { get; set; }

        //Koppling till SubCategory. Skapar främmande nyckel relationen till SubCategory
        // Lista av SubCategorys 
        public virtual ICollection<SubCategory> SubCategories { get; set; }

    }

}