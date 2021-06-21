using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Telegram.Bot;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Root> WifiList;
            Console.OutputEncoding = Encoding.Unicode;
            var ApiKey = "d3f63b4b55db0b86ef4eba7539e07204";
            //var City = "Kazan";
            var url = $"https://apidata.mos.ru/v1/datasets/2756/rows?api_key={ApiKey}";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                WifiList = JsonConvert.DeserializeObject<List<Root>>(result);
            }


            TelegramBotClient bot = new TelegramBotClient("1769451705:AAFDJB7I0fUwjzdLxtFXJqss_dl9CJI9rd4");
            bot.OnMessage += (s, arg) =>
            {
                string msg;
                var wifi = WifiList.Where(it => it.Cells.Location.Contains(arg.Message.Text)).Select(item => $"Название = {item.Cells.WiFiName}\nУлица = {item.Cells.Location}").ToArray();
                if (arg.Message.Text == "/start")
                    msg = "Введите улицу Москвы! \n Например : Басманная улица, Кутузовский проспект, Киевская улица, Ленинградский проспект , Пятницкая улица, Трёхпрудный переулок";
                else if (wifi.Length > 0)
                    msg = string.Join("\n", wifi);
                else
                    msg = "Напишите в чат команду /start";
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Бот говорит:\n { msg }");
            };

            bot.StartReceiving();
            Console.ReadKey();
        }
    }
}