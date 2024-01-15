using System.ComponentModel.DataAnnotations;

namespace ShabbatBrunch.Models;

public class Avis
{
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required(ErrorMessage = "Entrez un Nom valide de minimum 3 caractères")]
    public string? Auteur { get; set; }

    [StringLength(500, ErrorMessage = "La longueur maximale du commentaire est de 500 caractères.")]
    public string? Contenu { get; set; }
    public string? Note { get; set; }
    public DateTime DateAvis { get; set; }
}