using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProducts.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public int Role { get; set; } = 1; 

        public bool Block { get; set; } = false;

        public bool FirstAuth { get; set; } = false;
    }
}
