using System;

namespace WebApp.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime RegisteredOn { get; set; }
        public long PrintedUnits { get; set; }
    }
}