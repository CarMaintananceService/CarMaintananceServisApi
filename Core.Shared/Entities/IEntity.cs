using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Shared.Entities
{

	public interface INoIdentityEntity<TPrimaryKey>
	{
		/// <summary>
		/// Unique identifier for this entity.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		TPrimaryKey Id { get; set; }
	}

	public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }

    public interface IEntity : IEntity<int>
    {

    }
	public interface INoIdentityEntity : INoIdentityEntity<int>
	{

	}
}
