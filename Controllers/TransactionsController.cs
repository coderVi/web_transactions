using Microsoft.AspNetCore.Mvc;
using web_transactions.Models;

namespace web_transactions.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TransactionsModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.IlkSayi) || string.IsNullOrEmpty(model.IkinciSayi))
                {
                    model.SonucStr = "Tüm Alanlar Dolu Olmalı";
                    return View(model);
                }

                bool isFirstNumberValid = int.TryParse(model.IlkSayi, out int a);
                bool isSecondNumberValid = int.TryParse(model.IkinciSayi, out int b);

                if (!isFirstNumberValid || !isSecondNumberValid)
                {
                    model.SonucStr = "Geçersiz giriş.";
                    return View(model);
                }

                string action = Request.Form["submit"];
                if (action == "topla")
                {
                    model.Sonuc = a + b;
                }
                else if (action == "cikar")
                {
                    model.Sonuc = a - b;
                }
                else if (action == "carpma")
                {
                    model.Sonuc = a * b;
                }
                else if (action == "bolme")
                {
                    if (b == 0) // Bölme işleminde sıfıra bölme kontrolü
                    {
                        model.SonucStr = "0 ile bölme işlemi yapılamaz";
                        model.Sonuc = null;
                    }
                    else
                    {
                        model.Sonuc = a / b;
                        model.SonucStr = "";
                    }
                }
            }
            catch (Exception ex)
            {
                // Genel hata yönetimi
                model.SonucStr = $"Bir hata oluştu: {ex.Message}";
                model.Sonuc = null;
            }

            return View(model);
        }
    }
}
