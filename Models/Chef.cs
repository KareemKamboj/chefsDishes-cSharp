using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required(ErrorMessage = "is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "is required")]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "is required")]
    public DateTime DOB { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set;} = DateTime.Now;

    public List<Dish> ChefDishes { get; set; } = new List<Dish>();
}