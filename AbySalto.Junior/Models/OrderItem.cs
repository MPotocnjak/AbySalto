using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Models
{
    /// <summary>
    /// Predstavlja jednu stavku unutar narudzbe, ukljucujuci proizvod, kolicinu i cijenu
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Jedinstveni identifikator stavke narudzbe
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Naziv proizvoda narucenog od strane korisnika
        /// </summary>
        [Required(ErrorMessage = "Naziv proizvoda je obavezan.")]
        public string ProductName { get; set; }

        /// <summary>
        /// Kolicina narucenih jedinica tog proizvoda
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Kolièina mora biti barem 1.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Cijena pojedinacne jedinice proizvoda u EUR
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Cijena mora biti veæa od 0.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Identifikator narudzbe kojoj stavka pripada
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Referenca na narudzbu kojoj ova stavka pripada
        /// </summary>
        public Order? Order { get; set; }
    }
}
