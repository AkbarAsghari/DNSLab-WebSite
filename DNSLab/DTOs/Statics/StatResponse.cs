namespace DNSLab.DTOs.Statics
{
    public class StatResponse
    {
        public Response Response { get; set; }
    }

    public class Response
    {
        public Stats Stats { get; set; }
        public Mainchartdata MainChartData { get; set; }
        public Queryresponsechartdata QueryResponseChartData { get; set; }
        public Querytypechartdata Querytypechartdata { get; set; }
        public Topclient[] Topclients { get; set; }
        public Topdomain[] Topdomains { get; set; }
        public TopBlockedDomain[] TopBlockedDomains { get; set; }
    }

    public class Stats
    {
        public int TotalQueries { get; set; }
        public int TotalNoError { get; set; }
        public int TotalServerFailure { get; set; }
        public int TotalNxDomain { get; set; }
        public int TotalRefused { get; set; }
        public int TotalAuthoritative { get; set; }
        public int TotalRecursive { get; set; }
        public int TotalCached { get; set; }
        public int TotalBlocked { get; set; }
        public int TotalClients { get; set; }
        public int Zones { get; set; }
        public int CachedEntries { get; set; }
        public int AllowedZones { get; set; }
        public int BlockedZones { get; set; }
        public int AllowListZones { get; set; }
        public int BlockListZones { get; set; }
    }

    public class Mainchartdata
    {
        public string LabelFormat { get; set; }
        public DateTime[] Labels { get; set; }
        public Dataset[] Datasets { get; set; }
    }

    public class Dataset
    {
        public string Label { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public int BorderWidth { get; set; }
        public bool Fill { get; set; }
        public int[] Data { get; set; }
    }

    public class Queryresponsechartdata
    {
        public string[] Labels { get; set; }
        public Dataset1[] Datasets { get; set; }
    }

    public class Dataset1
    {
        public int[] Data { get; set; }
        public string[] BackgroundColor { get; set; }
    }

    public class Querytypechartdata
    {
        public string[] Labels { get; set; }
        public Dataset2[] Datasets { get; set; }
    }

    public class Dataset2
    {
        public int[] Data { get; set; }
        public string[] BackgroundColor { get; set; }
    }

    public class Topclient
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public int Hits { get; set; }
    }

    public class Topdomain
    {
        public string Name { get; set; }
        public int Hits { get; set; }
    }

    public class TopBlockedDomain
    {
        public string Name { get; set; }
        public int Hits { get; set; }
    }
}
