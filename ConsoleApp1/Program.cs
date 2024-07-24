using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace WhatsAppAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Список номеров и сообщение
            List<string> numbers = new List<string> { "+79991112233", "+79991112232", "79991112231" }; // Замените на ваши номера
            string message = "Здравствуйте! У нас есть интересные вакансии для вас. Подробности по ссылке: [ваша ссылка]";

            // Путь к WebDriver
            string driverPath = @"chromedriver.exe"; // Замените на путь к вашему chromedriver

            // Инициализация WebDriver
            IWebDriver driver = new ChromeDriver(driverPath);
            driver.Navigate().GoToUrl("https://web.whatsapp.com");

            // Время на сканирование QR-кода (увеличьте, если нужно)
            Thread.Sleep(20000);

            foreach (var number in numbers)
            {
                try
                {
                    // Переход на чат с номером
                    driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={number}&text={Uri.EscapeDataString(message)}");
                    Thread.Sleep(10000); // Время на загрузку страницы

                    // Клик на кнопку отправки сообщения
                    IWebElement sendButton = driver.FindElement(By.XPath("//button[@data-tab=\"11\"]"));
                    sendButton.Click();
                    Thread.Sleep(2000);

                    Console.WriteLine($"Сообщение отправлено на {number}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Не удалось отправить сообщение на {number}. Ошибка: {e.Message}");
                }
            }

            // Закрытие браузера
            driver.Quit();
        }
    }
}
