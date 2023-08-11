#pragma warning disable CS8618


using System.ComponentModel.DataAnnotations;
namespace ProductsAndCategoriesCore.Models;


public class Product
{
    //*KEY*
    [Key]
    // ProductId =========================
    public int ProductId { get; set; }


    // ProductName ========================= 
    [Required]
    // [MinLength(2, ErrorMessage = "Must be 2 characters long")]
    // [MaxLength(40, ErrorMessage = "No longer than 40 characters long")]
    public string ProductName { get; set; }


    // ProductDescription ======================== 
    [Required]
    // [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0!")]
    public string ProductDescription { get; set; }


    // ProductPrice ======================== 
    [Required]
    // [Range(1, 5, ErrorMessage = "You must include a Tastiness score between 1 - 5.")]
    public decimal ProductPrice { get; set; }


    // CreatedAt ======================== 
    public DateTime CreatedAt { get; set; } = DateTime.Now;



    // UpdatedAt ======================== 
    public DateTime UpdatedAt { get; set; } = DateTime.Now;



    //! association key ============================



    public List<Association> AssociationLinksCategories {get; set;} = new List<Association>();



}
