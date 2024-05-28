using Core.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {

    }

    /// <summary>
    /// Basic implementation of IEntity interface.
    /// An entity can inherit this class of directly implement to IEntity interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
		
		public virtual TPrimaryKey Id { get; set; }
    }


	/// <summary>
	/// Basic implementation of IEntity interface.
	/// An entity can inherit this class of directly implement to IEntity interface.
	/// </summary>
	/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
	[Serializable]
	public abstract class NoIdentityEntity<TPrimaryKey> : INoIdentityEntity<TPrimaryKey>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public virtual TPrimaryKey Id { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual TPrimaryKey No { get; set; }

	}




}
