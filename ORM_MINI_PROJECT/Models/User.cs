﻿namespace ORM_MINI_PROJECT.Models
{
    public class User:BaseEntity
    {
       
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
