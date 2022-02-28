using System.Collections.Generic;

namespace CleanArch.Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}