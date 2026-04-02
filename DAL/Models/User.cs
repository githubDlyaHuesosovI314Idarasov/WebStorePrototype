using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DAL.Models
{
    public class User : Entity
    {
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Boolean IsEmailVerified { get; set; } = false;
        public ComparedProducts ComparedProducts { get; set; } = null!;
        public FavoriteProducts FavoriteProducts { get; set; } = null!;
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();

    }
}
