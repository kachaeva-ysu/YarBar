using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YarBar.Models
{
    public class Place
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название заведения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [StringLength(4000, ErrorMessage = "Длина строки должна быть до 4000 символов")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Рейтинг")]
        public double Rating
        {
            get
            {
                double ratingSum = 0;
                int count = 0;
                if (Reviews == null || Reviews.Count() == 0)
                    return 0;
                foreach(var review in Reviews)
                {
                    ratingSum += review.Score;
                    count++;
                }
                return ratingSum / count;
            }
        }

        [HiddenInput(DisplayValue = false)]
        public int PlaceTypeId { get; set; }

        [Display(Name = "Тип")]
        public PlaceType? PlaceType { get; set; }

        public IEnumerable<Review>? Reviews { get; set; }
    }
}
