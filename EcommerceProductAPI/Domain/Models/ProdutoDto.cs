﻿namespace EcommerceProductAPI.Domain.Models
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
