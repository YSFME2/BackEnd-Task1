using Application.Abstractions;
using Domain.Common;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Persistence.Interceptors
{
    public class SaveChangesEntityInterceptor(ICurrentUserService CurrentUserService, TimeProvider TimeProvider) : SaveChangesInterceptor
    {

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateAuditableEntities(eventData.Context);
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateAuditableEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        void UpdateAuditableEntities(DbContext? context)
        {
            if (context == null) return;

            var dbContext = (AppDbContext)context!;
            foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    if (entry.Entity.IsDeleted)
                    {
                        entry.Entity.DeletedById = CurrentUserService.UserId;
                        entry.Entity.DeletedOn = TimeProvider.GetUtcNow();
                    }
                    else
                    {
                        entry.Entity.LastModifiedById = CurrentUserService.UserId;
                        entry.Entity.LastModifiedOn = TimeProvider.GetUtcNow();
                    }
                }
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedById = CurrentUserService.UserId;
                    entry.Entity.CreatedOn = TimeProvider.GetUtcNow();
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedById = CurrentUserService.UserId;
                    entry.Entity.DeletedOn = TimeProvider.GetUtcNow();
                }
            }

        }

    }
    static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
