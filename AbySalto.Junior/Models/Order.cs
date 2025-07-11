using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Models
{
    /// <summary>
    /// Predstavlja pojedinacnu korisnicku narudzbu, ukljucujuci proizvode, podatke za dostavu i nacin placanja
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Jedinstveni identifikator narudzbe
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ime i prezime kupca koji je izvrsio narudzbu
        /// </summary>
        [Required]
        public string CustomerName { get; set; }

        /// <summary>
        /// Trenutni status narudzbe (Na cekanju, U pripremi ili Zavrsena)
        /// </summary>
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.NaCekanju; // Enum

        /// <summary>
        /// Datum i vrijeme kada je narudzba kreirana (UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Odabrani nacin placanja za ovu narudzbu (gotovina, kartica ili online)
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; } // Enum

        /// <summary>
        /// Adresa za dostavu narucenih proizvoda, nije obavezna ako kupac osobno preuzima narudzbu
        /// </summary>
        public string? DeliveryAddress { get; set; }

        /// <summary>
        /// Telefonski broj za kontakt s kupcem
        /// </summary>
        public string? ContactPhone { get; set; }

        /// <summary>
        /// Dodatna napomena uz narudzbu, primjerice upute za dostavu
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Lista stavki unutar narudzbe — svaki proizvod ima kolicinu i cijenu
        /// </summary>
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        /// <summary>
        /// Ukupan iznos narudzbe, izracunat zbrajanjem svih stavki
        /// </summary>
        public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);

        /// <summary>
        /// Valuta u kojoj se narudzba obracunava (npr. EUR)
        /// </summary>
        public string Currency { get; set; } = "EUR";
    }

    /// <summary>
    /// Definira moguci status narudzbe tijekom procesa obrade
    /// </summary>
    public enum OrderStatus
    {
        /// <summary> Narudzba je zaprimljena, ali jos nije obradjena </summary>
        NaCekanju,

        /// <summary> Narudzba je u pripremi </summary>
        UPripremi,

        /// <summary> Narudzba je zavrsena i isporucena </summary>
        Zavrsena
    }

    /// <summary>
    /// Dostupne opcije placanja koje korisnik moze odabrati
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary> Placanje gotovinom pri dostavi ili preuzimanju </summary>
        Gotovina,

        /// <summary> Placanje karticom (na internetu ili prilikom preuzimanja) </summary>
        Kartica,

        /// <summary> Online placanje putem platforme </summary>
        Online
    }
}