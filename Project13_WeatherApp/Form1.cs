using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project13_WeatherApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city/istanbul/EN"),
                Headers =
    {
        { "x-rapidapi-key", "398cbc588dmsh5e724d7a6b4d99ap1cfd72jsn7ff4c9f0d9fa" },
        { "x-rapidapi-host", "open-weather13.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);
                var fahrenheit = json["main"]["feels_like"].ToString();
                var windSpeed = json["wind"]["speed"].ToString();
                var humidty = json["main"]["humidity"].ToString();
                lblFahrenheit.Text = fahrenheit;
                lblHumidity.Text = humidty;
                lblWindSpeed.Text = windSpeed;
                double celsius = (double.Parse(fahrenheit) - 32) / 1.8;
                lblCelsius.Text = celsius.ToString("0.00");//0.00 virgülden sonra sadece 2 basamak getirmek icin
            }
        }
    }
}
