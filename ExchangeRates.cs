namespace AssetTrackerEF
{
    /*
        Convenience class to set and get exchange rate data 
        used to convert prices to USD from SEK and EUR 
    */
    public class ExchangeRates
    {
        /*
            ExchangeRates class constructor
        */
        public ExchangeRates()
        {  
            LiveCurrency.FetchRates();

            RateEURUSD = LiveCurrency.Convert(1.0m, "EUR", "USD");
            RateSEKUSD = LiveCurrency.Convert(1.0m, "SEK", "USD");

            // Using fixed exchange rate if LiveCurrency call fails
            RateEURUSD = RateEURUSD == 0.0m ? 1.09m : RateEURUSD;
            RateSEKUSD = RateSEKUSD == 0.0m ? 0.096m : RateSEKUSD; 
        
        }

        /*
            RateEURUSD: The rate between EUR and USD
            RateSEKUSD: The rate between SEK and USD
        */
        public decimal RateEURUSD { get; set; } = 0.0m;
        public decimal RateSEKUSD { get; set; } = 0.0m;
    }

}