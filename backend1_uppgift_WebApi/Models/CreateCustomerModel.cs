using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend1_uppgift_WebApi.Models
{
    // Detta vill jag ha för att skapa en customer
    public class CreateCustomerModel
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string CustomerHash { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string AddressLine { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string ZipCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string City { get; set; }

    }
}
