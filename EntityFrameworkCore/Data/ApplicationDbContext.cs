using Business.Shared;
using Core.Entities;
using Core.Security;
using Core.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System;
using Cars.Models.Domain;

namespace EntityFrameworkCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(ApplicationDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        UserDataProvider _userDataProvider;



        public ApplicationDbContext SetUserDataProvider(UserDataProvider userDataProvider)
        {
            _userDataProvider = userDataProvider;
            return this;
        }



        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            if (entityType.BaseType != null)
                return;

            if (typeof(IHasActive).IsAssignableFrom(typeof(TEntity)))
            {
                //modelBuilder.Entity<TEntity>().Property<Boolean>("IsActive");
                modelBuilder.Entity<TEntity>().HasQueryFilter(e => EF.Property<bool>(e, "IsActive") == true);
            }

            //if (typeof(IHasCompany).IsAssignableFrom(typeof(TEntity)))
            //{
            //	modelBuilder.Entity<TEntity>().HasQueryFilter(e => EF.Property<int>(e, "CompanyId") == _companyId);
            //}
        }

        private void _logModifies(int userId)
        {
            try
            {
                var modifiedEntities = ChangeTracker.Entries()
                   .Where(p => p.State == EntityState.Modified).ToList();

                List<object> changes = new List<object>();
                foreach (var change in modifiedEntities)
                {
                    Dictionary<string, string> newValues = new Dictionary<string, string>();
                    Dictionary<string, string> oldValues = new Dictionary<string, string>();
                    foreach (IProperty prop in change.OriginalValues.Properties)
                    {
                        var originalValue = change.OriginalValues[prop.Name]?.ToString();
                        var currentValue = change.CurrentValues[prop.Name]?.ToString();
                        if (originalValue != currentValue)
                        {
                            newValues.Add(prop.Name, currentValue);
                            oldValues.Add(prop.Name, originalValue);
                        }
                    }

                    if (oldValues.Count > 0)
                    {
                        string entityName = change.Entity.GetType().Name;
                        string PrimaryKey = change.OriginalValues.Properties.FirstOrDefault(prop => prop.IsPrimaryKey() == true)?.Name;
                        changes.Add(new
                        {
                            UserId = userId,
                            EntityName = entityName,
                            PrimaryKeyValue = PrimaryKey == null ? "0" : change.OriginalValues[PrimaryKey].ToString(),
                            OldValues = string.Join(" , ", oldValues.Select(x => x.Key + " : " + x.Value)),
                            NewValues = string.Join(" , ", newValues.Select(x => x.Key + " : " + x.Value)),
                            Date = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                            State = "Update"
                        });
                    }

                    //InsertDataOperation()


                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void _logRemoves(int userId)
        {
            var removedEntities = ChangeTracker.Entries()
                   .Where(p => p.State == EntityState.Deleted).ToList();

            foreach (var removed in removedEntities)
            {
                string entityName = removed.Entity.GetType().Name;
                string PrimaryKey = removed.OriginalValues.Properties.FirstOrDefault(prop => prop.IsPrimaryKey() == true)?.Name;
                List<object> removeds = new List<object>();
                removeds.Add(new
                {
                    UserId = userId,
                    EntityName = entityName,
                    PrimaryKeyValue = PrimaryKey == null ? "0" : removed.OriginalValues[PrimaryKey].ToString(),
                    OldValue = Newtonsoft.Json.JsonConvert.SerializeObject(removed),
                    Date = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                    State = "Remove"
                });

            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_userDataProvider != null && _userDataProvider.IsAvailable())
            {
                int userId = _userDataProvider.Get_Id();

                //_logModifies(userId);
                //_logRemoves(userId);

                try
                {
                    var selectedEntityList = ChangeTracker.Entries()
                                                .Where(x => x.Entity is ICreationAudited &&
                                                (x.State == EntityState.Added));

                    foreach (var entity in selectedEntityList)
                    {
                        ((ICreationAudited)entity.Entity).CreationTime = DateTime.Now;
                        ((ICreationAudited)entity.Entity).CreatorUserId = userId;
                    }


                    selectedEntityList = ChangeTracker.Entries()
                                        .Where(x => x.Entity is IModificationAudited &&
                                        (x.State == EntityState.Modified));

                    foreach (var entity in selectedEntityList)
                    {
                        ((IModificationAudited)entity.Entity).LastModificationTime = DateTime.Now;
                        ((IModificationAudited)entity.Entity).LastModifierUserId = userId;
                    }

                    return base.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            if (_userDataProvider != null && _userDataProvider.IsAvailable())
            {
                int userId = _userDataProvider.Get_Id();

                //_logModifies(userId);
                //_logRemoves(userId);

                try
                {
                    var selectedEntityList = ChangeTracker.Entries()
                                                .Where(x => x.Entity is ICreationAudited &&
                                                (x.State == EntityState.Added));

                    foreach (var entity in selectedEntityList)
                    {
                        ((ICreationAudited)entity.Entity).CreationTime = DateTime.Now;
                        ((ICreationAudited)entity.Entity).CreatorUserId = userId;
                    }


                    selectedEntityList = ChangeTracker.Entries()
                                        .Where(x => x.Entity is IModificationAudited &&
                                        (x.State == EntityState.Modified));

                    foreach (var entity in selectedEntityList)
                    {
                        ((IModificationAudited)entity.Entity).LastModificationTime = DateTime.Now;
                        ((IModificationAudited)entity.Entity).LastModifierUserId = userId;
                    }

                    return base.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                return base.SaveChanges();
        }

        //public ApplicationDbContext()
        //{

        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        #region global

        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<RepairActivity> RepairActivitys { get; set; }
        public DbSet<StockCard> StockCards { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<CaseType> CaseTypes { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<StockCardUnit> StockCardUnits { get; set; }
        public DbSet<StockCardBrand> StockCardBrands { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<OutSourceLabor> OutSourceLabors { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        
        #endregion


        #region security

        public DbSet<User> Users { get; set; }

        #endregion


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("YourConnectionStringHere");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                ConfigureGlobalFiltersMethodInfo.MakeGenericMethod(entityType.ClrType).Invoke(this, new object[] { modelBuilder, entityType });




            base.OnModelCreating(modelBuilder);
        }






    }
}
