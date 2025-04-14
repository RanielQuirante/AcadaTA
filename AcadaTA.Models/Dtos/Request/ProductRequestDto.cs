using System.ComponentModel.DataAnnotations;

namespace AcadaTA.Models.Dtos.Request
{
    public class ProductRequestDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public required string Description { get; set; }

        [Range(1.00, double.MaxValue, ErrorMessage = "Price must be greater than 1.")]
        public required decimal Price { get; set; }
    }
}
