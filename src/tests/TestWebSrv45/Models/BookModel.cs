using System;
using System.Collections.Generic;

namespace TestWebSrv45.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime RegisteredOn { get; set; }
        public long PrintedUnits { get; set; }
        public List<string> Comments { get; set; }
        public List<BookReference> BookReferences { get; set; }
    }

    public class BookReference
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}