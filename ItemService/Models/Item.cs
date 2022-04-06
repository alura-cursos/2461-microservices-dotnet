using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class Item
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public double Preco { get; set; }

        [Required]
        public int IdRestaurante { get; set; }

        public Restaurante Restaurante { get; set; }
    }
}