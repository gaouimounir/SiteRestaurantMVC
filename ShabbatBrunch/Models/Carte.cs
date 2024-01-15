using System.ComponentModel.DataAnnotations;
using ShabbatBrunch.Enums;

namespace ShabbatBrunch.Models;

public class Carte
{
   
    public int Id { get; set; }
    public Categorie Categorie { get; set; }
    public List<CarteItem> CarteItems { get; set; } = new();
}