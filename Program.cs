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
