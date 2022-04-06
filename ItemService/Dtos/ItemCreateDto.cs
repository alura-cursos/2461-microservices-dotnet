using System.ComponentModel.DataAnnotations;

namespace ItemService.Dtos
{
    public class ItemCreateDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public double Preco { get; set; }
    }
}