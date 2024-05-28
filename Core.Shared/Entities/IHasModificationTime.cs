namespace Core.Shared.Entities
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}
