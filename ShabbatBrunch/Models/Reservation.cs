using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShabbatBrunch.Models;

public class Reservation
{
    public int Id { get; set; }


    [StringLength(60, MinimumLength = 3)]
    [Required(ErrorMessage = "Entrez un Nom valide de minimum 3 caractères")]
    public string? Nom { get; set; }


    [StringLength(60, MinimumLength = 3)]
    [Required(ErrorMessage = "Entrez un Prénom valide de minimum 3 caractères")]
    public string? Prenom { get; set; }


    [Required(ErrorMessage = "Entrez un Numéro de téléphone valide comprenant 10 chiffres")]
    public string? Telephone { get; set; }


    [Required(ErrorMessage = "Le nombre de couvert est requis.")]
    [CouvertsBetween1And10(ErrorMessage = "Une réservation est pour minimum 1 personne et est limitée a 10 couverts")]
    public int NbCouvert { get; set; }
    public DateOnly Date { get; set; }

    [Required(ErrorMessage = "Entrez une heure valide")]
    [TimeBetween10And15(ErrorMessage = "L'heure doit être entre 10h et 15h")]
    public TimeOnly Heure { get; set; }

    [StringLength(500, ErrorMessage = "La longueur maximale du commentaire est de 500 caractères.")]
    public string? Commentaire { get; set; }
}

public class TimeBetween10And15Attribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is TimeOnly)
        {
            var timeReservation = (TimeOnly)value;
            var startTime = new TimeOnly(10, 0); // 10h
            var endTime = new TimeOnly(15, 0);  // 15h

            return startTime <= timeReservation && timeReservation <= endTime;
        }

        return false; // Si le type n'est pas TimeOnly
    }
}


public class CouvertsBetween1And10Attribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is int)
        {
            var nombreCouvertReserve = (int)value;
            return 1 <= nombreCouvertReserve && nombreCouvertReserve <= 10;
        }

        return false; // Si le type n'est pas int
    }
}