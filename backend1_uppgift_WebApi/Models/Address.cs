using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend1_uppgift_WebApi.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string AddressLine { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string ZipCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string City { get; set; }

        // Skapar främmande nyckel relationen 
        // Listar upp Customers under Address. 
        public virtual ICollection<Customer> Customers { get; set; }


    }
}