﻿namespace Core.Shared.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
