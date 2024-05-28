﻿namespace Core.Shared.Entities
{
    public interface IDeletionAudited : IHasDeletionTime
    {
        long? DeleterUserId { get; set; }
    }

    public interface IDeletionAudited<TUser> : IDeletionAudited
        where TUser : IEntity<long>
    {

        TUser DeleterUser { get; set; }
    }
}
