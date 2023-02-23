namespace RecorridoHistoricoApi.Models.Base
{
    public interface IEntityBase
    {
        int Id { get; set; }

        DateTime? DeletedAt { get; set; }
    }
}
