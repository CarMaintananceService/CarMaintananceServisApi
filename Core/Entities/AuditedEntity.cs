using Core.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    
	public abstract class AuditedEntity : AuditedEntity<int>, IEntity
	{

	}


	public abstract class AuditedNoIdentityEntity<TPrimaryKey> : CreationAuditedNoIdentityEntity<TPrimaryKey>, IAudited
	{

		public virtual DateTime? LastModificationTime { get; set; }

		public virtual long? LastModifierUserId { get; set; }
	}



	public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }
    }


    public abstract class AuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey>, IAudited<TUser>
        where TUser : IEntity<long>
    {
        [ForeignKey("CreatorUserId")]
        public virtual TUser CreatorUser { get; set; }

        [ForeignKey("LastModifierUserId")]
        public virtual TUser LastModifierUser { get; set; }
    }
}
