namespace EdecanesV2.Utils.DataTable
{
    public enum DtOrderDir
    {
        Asc,
        Desc
    }


    public class DtOrder
    {
        public int Column { get; set; }

        public DtOrderDir Dir { get; set; }
    }
}
