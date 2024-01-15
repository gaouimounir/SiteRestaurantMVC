using ShabbatBrunch.Enums;

namespace ShabbatBrunch.Models;

public class CarteItem
{
    public int Id { get; set; }
    public string? Nom { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Prix { get; set; }
    public Categorie? Categorie { get; set; } 
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Carte? Carte { get; set; }
    public Allergenes? Allergenes { get; set; }
}