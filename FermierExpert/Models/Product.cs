﻿namespace FermierExpert.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
    }
}
