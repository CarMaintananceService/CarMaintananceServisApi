using Core.Shared.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public abstract class CreationAuditedEntity : CreationAuditedEntity<int>, IEntity
    {

    }


	
    public abstract class CreationAuditedNoIdentityEntity<TPrimaryKey> : NoIdentityEntity<TPrimaryKey>, ICreationAudited
	{
		[DefaultValue("getdate()")]
		public virtual DateTime CreationTime { get; set; }

		public virtual long? CreatorUserId { get; set; }

		protected CreationAuditedNoIdentityEntity()
		{
			CreationTime = DateTime.Now;
		}
	}


	public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
		[DefaultValue("getdate()")]
		public virtual DateTime CreationTime { get; set; }

        public virtual long? CreatorUserId { get; set; }

        protected CreationAuditedEntity()
        {
            CreationTime = DateTime.Now;
        }
    }

    public abstract class CreationAuditedEntity<TPrimaryKey, TUser> : CreationAuditedEntity<TPrimaryKey>, ICreationAudited<TUser>
        where TUser : IEntity<long>
    {

        [ForeignKey("CreatorUserId")]
        public virtual TUser CreatorUser { get; set; }
    }
}
