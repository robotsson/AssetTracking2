using Microsoft.EntityFrameworkCore;
using static System.Console;
using static System.StringComparison;

namespace AssetTrackerEF
{
    /*
        General class for user interaction and creation and handling of asset list
    */
    public class AssetTrackerEF
    {
      //  private List<Asset> assets;
        private ExchangeRates exchangeRates;
        private AssetTrackerContext assetDb; 

        public AssetTrackerEF()
        {
            // assets = [];
            assetDb = new AssetTrackerContext();
            exchangeRates = new();
            WriteLine("Welcome to AssetTracker 2.0 - Entity Framework Edition.");
        }


        /*
            Prints the asset list in a formatted way with color highlighting depending
            on if write-off date is getting closer

            in: sortByOffice    Set to false to sort by Asset type, otherwise sorted by in which location asset is in
        */
        public void PrettyPrint( bool sortByOffice = true )
        {
            // List<Asset> assets = assetDb.Assets.ToList();

            if( !assetDb.Assets.Any() )
            {
                WriteLine("\nAsset db is empty!");
                return; 
            }

            // if sortByOffice is true:  set sortedAssets to assets sorted by Office first
            // if sortByOffice is false: set sortedAsset to assets sorted by Asset type first
            // secondary sort criteria is Date Purchased in both cases
            List<Asset> sortedAssets = 
                sortByOffice ? 
                    assetDb.Assets.OrderBy(x => x.Office )
                          .ThenBy(x => x.DatePurchased)
                          .ToList()
                    :
                    assetDb.Assets.OrderBy(x => EF.Property<string>(x, "Discriminator") )
                          .ThenBy(x => x.DatePurchased)
                          .ToList();
       
            WriteLine 
            ( 
                "Type".PadRight(15) +
                "Brand".PadRight(15) +
                "Model".PadRight(20) +
                "Price".PadRight(20) +
                "US Price".PadRight(20) +
                "Date Purchased".PadRight(18) +
                "Office" 
            ); 

            WriteLine
            ( 
                "----".PadRight(15) +
                "-----".PadRight(15) +
                "-----".PadRight(20) +
                "-----".PadRight(20) +
                "--------".PadRight(20) +
                "--------------".PadRight(18) +
                "------"
            );

            foreach (Asset asset in sortedAssets)
            {
                if( asset.MarkedRed() )
                {
                    ForegroundColor = ConsoleColor.Red;
                }
                else if( asset.MarkedYellow() )
                {
                    // My console background color is white, Yellow is barely visible
                    ForegroundColor = ConsoleColor.Magenta;
                }

                WriteLine
                ( 
                    asset.GetType().Name.PadRight(15) +
                    asset.Brand?.PadRight(15) +
                    asset.Model?.PadRight(20) +
                    (asset.Price.ToString("N2") + " " + asset.Currency).PadRight(20) +
                    (PriceInUSD( asset.Price, asset.Currency, exchangeRates ).ToString("N2") + " " + "USD").PadRight(20) +
                    asset.DatePurchased.ToString("yyyy-MM-dd").PadRight(18) +
                    asset.Office
                );

                ResetColor();
            }
        }

        /* helper function to convert prices to usd */
        private static decimal PriceInUSD( decimal price, string? currency, ExchangeRates exchangeRates )
        {
            return currency switch
            {
                "EUR" => price * exchangeRates.RateEURUSD,
                "SEK" => price * exchangeRates.RateSEKUSD,
                _ => price
            };
        }

        /*
            Helper method to make it easy to add a lot of assets into the asset db
        */
        public void InsertSampleData()
        {
         
            List<Asset> assets = [
                new Computer { Brand = "HP", Model = "Elitebook", Price = 1176.03m, Currency = "EUR", DatePurchased = new DateOnly( 2019, 6, 1 ), Office = "Spain" },
                new Computer { Brand = "Asus", Model = "W234", Price = 4900m, Currency = "SEK", DatePurchased = new DateOnly( 2020, 10, 2 ), Office = "Sweden" },
                new Computer { Brand = "Lenovo", Model = "Yoga 730", Price = 835m, Currency = "USD", DatePurchased = new DateOnly( 2018, 5, 28 ), Office = "USA" },
                new Phone { Brand = "Apple", Model = "Iphone 15", Price = 10000m, Currency = "USD", DatePurchased = new DateOnly( 2024, 9, 11 ), Office = "USA" },
                new Computer { Brand = "Lenovo", Model = "Yoga 530", Price = 1030m, Currency = "USD", DatePurchased = new DateOnly( 2019, 5, 21 ), Office = "USA" },
                new Computer { Brand = "Apple", Model = "Macbook Pro", Price = 970m, Currency = "EUR", DatePurchased = new DateOnly( 2022, 7, 13 ), Office = "Spain" },
                new Phone { Brand = "Apple", Model = "iPhone", Price = 818.18m, Currency = "EUR", DatePurchased = new DateOnly( 2020, 9, 25 ), Office = "Spain" },
                new Phone { Brand = "Apple", Model = "iPhone", Price = 10375m, Currency = "SEK", DatePurchased = new DateOnly( 2018, 7, 15 ), Office = "Sweden" },
                new Phone { Brand = "Motorola", Model = "Razr", Price = 8083.33m, Currency = "SEK", DatePurchased = new DateOnly( 2022, 5, 16 ), Office = "Sweden" },
                new Phone { Brand = "Samsung", Model = "Galaxy S23", Price = 8083.33m, Currency = "SEK", DatePurchased = new DateOnly( 2023, 3, 16 ), Office = "Sweden" },
                new Computer { Brand = "Asus", Model = "ROG 500", Price = 9999.90m, Currency = "SEK", DatePurchased = new DateOnly( 2024, 10, 15 ), Office = "Sweden" },
                new Phone { Brand = "Nokia", Model = "3310", Price = 160.11m, Currency = "EUR", DatePurchased = new DateOnly( 2019, 5, 16 ), Office = "Germany" },
                new Phone { Brand = "Xiaomi", Model = "14 Ultra", Price = 808.08m, Currency = "EUR", DatePurchased = new DateOnly( 2023, 3, 16 ), Office = "France" } 
            ];
            
            assetDb.AddRange( assets );
            assetDb.SaveChanges();

            WriteLine($"\nTest data added to list, Asset list now has {assetDb.Assets.Count()} items.");
        }


        public void DeleteAllAssets()
        {
            WriteLine("Deleting all assets in database");
            assetDb.Assets.RemoveRange( assetDb.Assets.ToList() );
            assetDb.SaveChanges();
        }


        // For code review: 
        // This method is a bit long, and too deep, would like to refactor
        // to make it a little bit flatter and maybe divide it into more
        // functions

        /*
            Helper function for Add asset
            return: true if asset was added, false if user aborted
        */ 
        public bool AddAssetProperties( Asset asset )
        {
            // Brand property input loop
            bool done = false;
            while( !done )
            {
                Write("Enter brand name: ");
                string? input = ReadLine()?.Trim();
                if( input is not null)
                {
                    if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        return false;
                    }
                    else if( input.Length != 0 )
                    {
                        asset.Model = input;
                        done = true;
                    }

                }
            }

            // Model property input loop
            done = false;
            while( !done )
            {
                Write("Enter model name: ");
                
                string? input = ReadLine()?.Trim();
                if( input is not null)
                {
                    if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        return false;
                    }
                    else if( input.Length != 0 )
                    {
                        asset.Model = input;
                        done = true;
                    } 

                }
            }

            // Office (Countries) user input loop
            done = false;
            while( !done )
            {
                Write("Enter Office (France, Germany, Spain, Sweden or USA): ");
                string? input = ReadLine()?.Trim();

                if( input is not null )
                {
                    if( input.Length == 0 )
                    {   
                    }
                    else if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        return false;
                    }
                    else
                    {               
                        if( input.Equals("Spain", OrdinalIgnoreCase ) )
                        {
                            asset.Office = "Spain";
                            done = true;
                        }
                        if( input.Equals("Germany", OrdinalIgnoreCase ) )
                        {
                            asset.Office = "Germany";
                            done = true;
                        }
                        if( input.Equals("France", OrdinalIgnoreCase ) )
                        {
                            asset.Office = "France";
                            done = true;
                        }
                        else if( input.Equals("Sweden", OrdinalIgnoreCase ) )
                        {   
                            asset.Office = "Sweden";
                            done = true;
                        }   
                        else if( input.Equals("USA", OrdinalIgnoreCase ) ) 
                        {
                            asset.Office = "USA";
                            done = true;
                        }
                        else
                        {
                            WriteLine("Not a valid office!");                          
                        }
                    }
                }
            }

            // Price property input loop
            done = false;
            while( !done )
            {
                Write("Enter amount paid: ");

                string? input = ReadLine()?.Trim();
                if( input is not null )
                {
                    if( input.Equals("Q", OrdinalIgnoreCase ) )
                    {
                        return false;
                    }

                    if( decimal.TryParse(input, out decimal amount) )
                    {
                        string currency = "USD"; // Default to USD
                        
                        if( asset.Office == "Spain" || asset.Office == "France" || asset.Office == "Germany" )   
                        {
                            currency = "EUR";
                        }

                        if( asset.Office == "Sweden" )
                        {
                            currency = "SEK";
                        }
                        
                        asset.Price = amount;
                        asset.Currency = currency; 
                        done = true;
                    }
                    else
                    {
                        WriteLine("Invalid amount value entered!");
                    }
                }
            }

            // DateOnly user input loop
            // asset.DatePurchased = DateOnly.FromDateTime(DateTime.Today); 
            done = false;
            while( !done )
            {
                Write("Enter Purchase date in YYYY-MM-DD format or Today for today's date: ");
                string? input = ReadLine()?.Trim();   

                if( input is not null )
                {
                    if( input.Length == 0 )
                    {
                    }
                    else if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        return false;
                    }
                    else if( input.Equals("Today", OrdinalIgnoreCase) )
                    {
                        asset.DatePurchased = DateOnly.FromDateTime(DateTime.Today);
                        done = true;    
                    }
                    else 
                    {
                        if( DateOnly.TryParse( input, out DateOnly date ))
                        {
                            asset.DatePurchased = date;
                            done = true;
                        }
                        else
                        {
                            WriteLine("Was not able to understand the date format. Please try again!");
                        }
                    }
                }
            }
    
            assetDb.Assets.Add(asset);
            assetDb.SaveChanges();

            return true;
        }


        /*
            Helper method to add an asset from user input
        */ 
        public void AddAsset()
        {     
            bool done = false;
            while( !done )
            {
                Write("\nPlease enter asset type, (C)omputer, (P)hone or (Q)uit to return to Main Menu: ");

                string? input = ReadLine()?.Trim();

                if( input is not null )
                {
                    Asset? asset = null; 

                    if( input.Equals("Computer", OrdinalIgnoreCase) || input.Equals("C", OrdinalIgnoreCase) )                                 
                    {
                        asset = new Computer();
                    }
                    else if( input.Equals("Phone", OrdinalIgnoreCase) || input.Equals("P", OrdinalIgnoreCase) )
                    {
                        asset = new Phone();
                    }
                    else if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        WriteLine( "Returning to Main Menu." );
                        done = true;
                    }
                    else
                    {
                        WriteLine( "Unknown Asset Type! ");
                    }

                    if(asset is not null)
                    {
                        if( AddAssetProperties(asset) )
                        {
                            WriteLine($"New asset added! Number assets in list: {assetDb.Assets.Count()}");
                        }
                        else
                        {
                            WriteLine("Aborted! Returning to Main Menu.");
                            return; 
                        }
                    }
                }    
            }
        }

        public void UpdateAsset()
        {
            WriteLine("Update asset");
        }

        public void DeleteAsset()
        {
            WriteLine("Delete asset");
        }

        /*
            Helper Method to display all possible menu choices
        */
        public void ListCommands()
        {
            WriteLine("\n  C - Create new asset");
            WriteLine("  R - Read and Print all assets (sorted by type)");
            WriteLine("  U - Update Asset");
            WriteLine("  D - Delete Asset");

            WriteLine();
            WriteLine("  O - Read and Print all assets (sorted by Office)");
            WriteLine("  S - Show asset database stats");
            WriteLine("  Q - Quit");
            WriteLine();
            Write("Enter menu choice: ");
        }

        /*
            Summarizes and prints some statistics about the current asset list state
        */
        public void ShowStats()
        {
            // List<Asset> assets = assetDb.Assets.ToList();

            int count = assetDb.Assets.Count();
            int computers = assetDb.Assets.OfType<Computer>().Count();
            int phones = assetDb.Assets.OfType<Phone>().Count();

            int offices = assetDb.Assets.Select( asset => asset.Office ).Distinct().Count();


            DateOnly red =  DateOnly.FromDateTime(DateTime.Now).AddMonths(-30);
            DateOnly yellow = DateOnly.FromDateTime(DateTime.Now).AddMonths(-27);

            int redItems = assetDb.Assets.Where( asset => asset.DatePurchased <= red ).Count();
            int yellowItems = assetDb.Assets.Where( asset => asset.DatePurchased > red && 
                                                             asset.DatePurchased <= yellow ).Count();

            WriteLine();
            WriteLine( $"Computers: {computers}" );
            WriteLine( $"   Phones: {phones}" );
            WriteLine( $"    Total: {count}" );

            WriteLine();
            WriteLine( $"Items that are within 3 months to write-off: {redItems}" );
            WriteLine( $"Items that are within 6 months (but not within 3) to write-off: {yellowItems}" );

            WriteLine($"\nAssets spread over {offices} offices.\n");
        }

        /*
            Run method contains "Main Menu" to and reads user input
            to perform actions:

            C - Add new asset 
            R - Read and print all assets (sorted by type)
            U - Update asset
            D - Delete asset

            O - Read and print all assets (sorted by Office)
            S - Show asset list stats
            Q - Quit

            Commands for Development only
            F - Fill database table with assets
            W - Remove all assets

            Q - Quit

            return: false if user entered Q to quit, true otherwise
        */
        public bool Run()
        {
            ListCommands();
            
            string? input = ReadLine()?.Trim().ToUpper();
                  
            switch( input )
            {
                case "C":  
                    AddAsset();
                    break;
            
                case "R":
                    PrettyPrint( false );
                    break;

                case "U":
                    UpdateAsset();
                    break;

                case "D":
                    DeleteAsset();
                    break;                    

                case "O":
                    PrettyPrint();
                    break;

                case "F":
                    InsertSampleData();           
                    break;

                case "W":
                    DeleteAllAssets();
                    break;

                case "S":
                    ShowStats(); 
                    break;

                case "Q":
                    WriteLine("\nGoodbye!");
                    return false;

                default:
                    break;
            }

            return true; 
        }
    }
}