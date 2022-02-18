using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkExample.Services
{
    public interface IEventsDbContext
    {
        void HandleSaveChangesFailed(object sender, SaveChangesFailedEventArgs args);
        void HandleSavedChanges(object sender, SavedChangesEventArgs args);
        void HandleSavingChanges(object sender, SavingChangesEventArgs args);
        void HandleStateChange(object sender, EntityStateChangedEventArgs args);
        void HandleTracked(object sender, EntityTrackedEventArgs args);
    }

    public class EventsDbContext: IEventsDbContext
    {
        private readonly ILogger<EventsDbContext> logger;

        public EventsDbContext(ILogger<EventsDbContext> logger)
        {
            this.logger = logger;
        }

        public void HandleTracked(object sender, EntityTrackedEventArgs args)
        {
            var message = $"Entity: {args.Entry.Entity}, state: {args.Entry.State}";
            logger.LogInformation(message);
        }

        public void HandleStateChange(object sender, EntityStateChangedEventArgs args)
        {
            var message = $@"Entity: {args.Entry.Entity}, prev state: {args.OldState} 
                        - new state: {args.NewState}";
            logger.LogInformation(message);
        }

        public void HandleSavingChanges(object sender, SavingChangesEventArgs args)
        {
            var Entitites = ((ApplicationDbContext)sender).ChangeTracker.Entries();

            foreach (var entidad in Entitites)
            {
                var message = $"Entity: {entidad.Entity} to {entidad.State}";
                logger.LogInformation(message);
            }
        }

        public void HandleSavedChanges(object sender, SavedChangesEventArgs args)
        {
            var message = $"{args.EntitiesSavedCount} Entitites processed";
            logger.LogInformation(message);
        }

        public void HandleSaveChangesFailed(object sender, SaveChangesFailedEventArgs args)
        {
            logger.LogError(args.Exception, "Error in SaveChanges");
        }
    }
}
