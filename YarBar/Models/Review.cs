using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YarBar.Models
{
    public class Review
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Оценка")]
        public int Score { get; set; }

        [Display(Name = "Комментарий")]
        [StringLength(4000, ErrorMessage = "Длина строки должна быть до 4000 символов")]
        public string? Comment { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PlaceId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
    }
}
