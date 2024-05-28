namespace Core.Shared.Entities
{
    public interface IModificationAudited : IHasModificationTime
    {
        long? LastModifierUserId { get; set; }
    }

    public interface IModificationAudited<TUser> : IModificationAudited
        where TUser : IEntity<long>
    {
        TUser LastModifierUser { get; set; }
    }
}
