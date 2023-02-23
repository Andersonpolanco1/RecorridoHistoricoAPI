using Newtonsoft.Json;

namespace EdecanesV2.Utils.DataTable
{
    public abstract class DtRow
    {
        [JsonProperty("DT_RowId")]
        public virtual string? DtRowId => null;


        [JsonProperty("DT_RowClass")]
        public virtual string? DtRowClass => null;


        [JsonProperty("DT_RowData")]
        public virtual object? DtRowData => null;


        [JsonProperty("DT_RowAttr")]
        public virtual object? DtRowAttr => null;
    }
}
