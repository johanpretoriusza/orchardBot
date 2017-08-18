using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using Noobot.Core.MessagingPipeline.Response;

namespace OrchidBot.Middleware
{
    class WeatherMiddleware
    {
        public WeatherMiddleware(string City)
        {
            city = City;
        }
        private string city;
        private float temp;
        private float tempMax;
        private float tempMin;

        public void CheckWeather()
        {
            WeatherAPI DataAPI = new WeatherAPI(City);
            temp = DataAPI.GetTemp();
        }

        public string City { get => city; set => city = value; }
        public float Temp { get => temp; set => temp = value; }
        public float TempMax { get => tempMax; set => tempMax = value; }
        public float TempMin { get => tempMin; set => tempMin = value; }
        public static WeatherMiddleware WarsawWeather { get; internal set; }
    }
    class WeatherAPI
    {
        public WeatherAPI(string city)
        {
            SetCurrentURL(city);
            xmlDocument = GetXML(CurrentURL);
        }

        public float GetTemp()
        {
            XmlNode temp_node = xmlDocument.SelectSingleNode("//temperature");
            XmlAttribute temp_value = temp_node.Attributes["value"];
            string temp_string = temp_value.Value;
            return float.Parse(temp_string);
        }

        private const string API_KEY = "84653d26555ed8314e0aa1e7cde81b66";
        private string CurrentURL;
        private XmlDocument xmlDocument;

        private void SetCurrentURL(string location)
        {
            CurrentURL = "http://api.openweathermap.org/data/2.5/weather?q="
                + location + "&mode=xml&units=metric&APPID=" + API_KEY;
        }

        private XmlDocument GetXML(string CurrentURL)
        {
            using (WebClient client = new WebClient())
            {
                string xmlContent = client.DownloadString(CurrentURL);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlContent);
                return xmlDocument;
            }
        }
    }
}