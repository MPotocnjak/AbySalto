# 🧾 Restaurant Order System (.NET)

Rješenje zadatka za upravljanje narudžbama restorana, temeljeno na originalnom projektu [`Abysalto/junior.net`](https://github.com/Abysalto/junior.net).

---

## 📦 Ključne funkcionalnosti

- ✅ Dodavanje novih narudžbi
- ✅ Pregled svih narudžbi
- ✅ Ažuriranje statusa narudžbi (`NaCekanju`, `UPripremi`, `Zavrsena`)
- ✅ Izračun ukupnog iznosa narudžbe
- ✅ Sortiranje narudžbi po iznosu

---

## 🧱 Struktura baze

- `Order` – informacije o kupcu i dostavi te status, vrijeme i ostali detalji narudžbe
- `OrderItem` – naziv artikla, količina i cijena

---

## ⚙️ Tehnologije

- ASP.NET Core 8
- Entity Framework Core
- SQL Server LocalDb
- Swagger UI

---

## 🚀 Pokretanje aplikacije

1. Kloniraj repozitorij:
   ```bash
   git clone https://github.com/MPotocnjak/AbySalto.git

2. Uđi u mapu repozitorija:
   ```bash
   cd AbySalto/AbySalto.Junior

3. Pokreni migracije:
   ```bash
   dotnet ef database update

4. Pokreni aplikaciju:
   ```bash
   dotnet run

5. Otvori Swagger UI:
   https://localhost:7056 ili http://localhost:5074

---
## 💡 Primjer JSON narudžbe

   ```bash
{
  "customerName": "Ana Marić",
  "createdAt": "2025-07-11T13:45:00Z",
  "deliveryAddress": "Gundulićeva 14, Rijeka",
  "contactNumber": "+385 91 123 4567",
  "note": "Bez luka.",
  "status": "NaCekanju",
  "paymentMethod": "Gotovina",
  "currency": "EUR",
  "items": [
    {
      "productName": "Burger Classic",
      "quantity": 1,
      "price": 7.90
    },
    {
      "productName": "Sok od jabuke",
      "quantity": 2,
      "price": 2.50
    }
  ]
}
   ```

---

Autorica: Magdalena Potočnjak