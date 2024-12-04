using System.Xml;

namespace AssetTrackerEF
{

    public class CurrencyObj
    {
        public string CurrencyCode { get; set; } 
        public decimal ExchangeRateFromEUR { get; set; }

        public CurrencyObj( string currencyCode, decimal exchangeRateFromEUR )
        {
            CurrencyCode = currencyCode;
            ExchangeRateFromEUR = exchangeRateFromEUR;
        }  
    }
    public class LiveCurrency // Class that handles fetching the exchange rates and converting currencies
    {
        private static List<CurrencyObj> CurrencyList = [];


        public static void FetchRates()
        {
            string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"; // Exchange rate XML document

            XmlTextReader reader = new XmlTextReader(url);
            while (reader.Read()) // Goes through the XML document and saves the currency exchange rates to the local list
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    while (reader.MoveToNextAttribute()) 
                    {
                        if (reader.Name == "currency") // Identifies each currency attribute and saves the currency code and rate as an object
                        {
                            string currencyCode = reader.Value;

                            reader.MoveToNextAttribute();
                            decimal rate = decimal.Parse(reader.Value);
                            CurrencyList.Add(new CurrencyObj(currencyCode, rate));
                        }
                    }
                }
            }
        }

        public static decimal Convert(decimal input, string fromCurrency, string toCurrency) // Method that uses the fetched rates to convert between the given rates via Euro
        {
            decimal? value = 0.0m;
            
            if (fromCurrency == "EUR")
            {
                value = input * CurrencyList?.Find(c => c.CurrencyCode == toCurrency)?.ExchangeRateFromEUR;
            }
            else if (toCurrency == "EUR")
            {
                value = input / CurrencyList?.Find(c => c.CurrencyCode == fromCurrency)?.ExchangeRateFromEUR;
            }
            else
            {
                value = input / CurrencyList?.Find(c => c.CurrencyCode == fromCurrency)?.ExchangeRateFromEUR;
                value *= CurrencyList?.Find(c => c.CurrencyCode == toCurrency)?.ExchangeRateFromEUR;
            }

           // return (decimal)(value is null ? 0.0m : value);
           return value ?? 0.0m;
        }
    }
}