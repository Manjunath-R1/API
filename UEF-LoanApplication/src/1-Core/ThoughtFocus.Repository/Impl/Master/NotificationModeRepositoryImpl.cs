namespace ThoughtFocus.Repository.Impl.Master
{ 
    using DataAccess.Models.Master;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class NotificationModeRepositoryImpl : AbstractEFApplicationBaseRepository<NotificationMode>, INotificationModeRepositoryImpl
    {
        #region Constructors

        public NotificationModeRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}