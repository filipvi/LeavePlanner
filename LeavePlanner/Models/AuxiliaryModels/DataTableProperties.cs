namespace LeavePlanner.Models.AuxiliaryModels
{
    public class DataTableProperties
    {
        public string Search { get; set; }
        public string Draw { get; set; }
        public string Order { get; set; }
        public string OrderDir { get; set; }
        public int StartRec { get; set; }
        public int PageSize { get; set; }

        public int TotalRecords { get; set; }
        public int RecFilter { get; set; }
    }
}
