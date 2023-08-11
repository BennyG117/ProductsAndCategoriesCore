#pragma warning disable CS8618


using System.ComponentModel.DataAnnotations;
namespace ProductsAndCategoriesCore.Models;


public class Category
{
    //*KEY*
    [Key]
    // CategoryId =========================
    public int CategoryId { get; set; }


    // CategoryName ========================= 
    [Required]
    // [MinLength(2, ErrorMessage = "Must be 2 characters long")]
    // [MaxLength(40, ErrorMessage = "No longer than 40 characters long")]
    public string CategoryName { get; set; }


    // CreatedAt ======================== 
    public DateTime CreatedAt { get; set; } = DateTime.Now;



    // UpdatedAt ======================== 
    public DateTime UpdatedAt { get; set; } = DateTime.Now;



    //! Association key ============================

    public List<Association> AssociationLinksProducts {get; set;} = new List<Association>();



}
