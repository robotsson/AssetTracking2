namespace AssetTrackerEF {
    public class Asset
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public decimal Price { get; set; }
        public string? Currency { get; set; }
        public DateOnly DatePurchased { get; set; }
        public string? Office { get; set; } 

        public bool MarkedRed()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            return this.DatePurchased.AddMonths(30).CompareTo( today ) <= 0;  
        }

        public bool MarkedYellow()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            if( !this.MarkedRed() )
            {
                return this.DatePurchased.AddMonths(27).CompareTo( today ) <= 0; 
            } 
            else
            { 
                return false;
            }
        }

    }

    public class Phone : Asset {}
    public class Computer : Asset {}

}
