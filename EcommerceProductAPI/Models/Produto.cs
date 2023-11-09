namespace EcommerceProductAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public string Tamanho { get; set; }
        public EGenero Genero { get; set; }
    }

    public enum EGenero
    {
        Masculino = 1,
        Feminino = 2,
    }
}
