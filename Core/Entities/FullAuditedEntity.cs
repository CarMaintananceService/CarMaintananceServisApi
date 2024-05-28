﻿using Core.Shared.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public abstract class FullAuditedEntity : FullAuditedEntity<int>, IEntity
    {

    }


	
	[Serializable]
	public abstract class FullAuditedNoIdentityEntity<TPrimaryKey> : AuditedNoIdentityEntity<TPrimaryKey>, IFullAudited
	{
		[DefaultValue(false)]
		public virtual bool IsDeleted { get; set; }

		public virtual long? DeleterUserId { get; set; }

		public virtual DateTime? DeletionTime { get; set; }
	}
	

	[Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited
    {
		[DefaultValue(false)]
		public virtual bool IsDeleted { get; set; }

        public virtual long? DeleterUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }
    }

    public abstract class FullAuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey, TUser>, IFullAudited<TUser>
        where TUser : IEntity<long>
    {
        public virtual bool IsDeleted { get; set; }

        [ForeignKey("DeleterUserId")]
        public virtual TUser DeleterUser { get; set; }

        public virtual long? DeleterUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }
    }
}