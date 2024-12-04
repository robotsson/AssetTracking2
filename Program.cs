using System.Globalization;

namespace AssetTrackerEF
{
    public class Program
    {
        /*
            Main Program entry point
        */
        private static void Main(string[] args)
        {
            // decimal.Parse does not like handling numbers that use "." as separator when CultureInfo has it set to ","
            if( CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Equals(",") )
            {
            //    var mixedCulture = new CultureInfo( CultureInfo.CurrentCulture.Name, false );            
            //    mixedCulture.NumberFormat.NumberDecimalSeparator = ".";
            //    CultureInfo.CurrentCulture = mixedCulture;
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            }
          
            AssetTrackerEF assetTracker = new();
            while( assetTracker.Run() );
        }


    }
}




// var AssetDb = new AssetTrackerContext();

// var Assets = AssetDb.Assets.ToList();
// Assets.ForEach( x => AssetDb.Assets.Remove(x) );

// Computer computer = new()
// {   
//     Brand = "HP",
//     Model = "Elitebook",
//     DatePurchased = new DateOnly( 2019, 6, 1 ),
//     Price = 599.0m, 
//     Currency = "USD" 
// };

// Phone phone = new()
// {   
//     Brand = "Nokia",
//     Model = "3310",
//     DatePurchased = new DateOnly( 2017, 6, 1 ),
//     Price = 1000.0m, 
//     Currency = "SEK" 
// };

// AssetDb.Assets.Add( computer );
// AssetDb.Assets.Add( phone );


// AssetDb.SaveChanges();

// Assets = AssetDb.Assets.ToList();

// Assets.ForEach( x => Console.WriteLine( x.Brand ) );