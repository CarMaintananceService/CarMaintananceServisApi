namespace Core.Shared.Entities
{
    public interface IAudited : ICreationAudited, IModificationAudited
    {

    }

    public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}
