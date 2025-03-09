namespace StudentFiles.Domain.Common.Entities
{
    using MediatR;

    public abstract class AggregateRoot(int id, Guid uid, bool isDeleted) : Entity(id: id, uid: uid, isDeleted: isDeleted)
    {
        private List<INotification> _domainEvents;

        public IReadOnlyList<INotification> DomainEvents => _domainEvents;

        public virtual void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(item: domainEvent);
        }

        public virtual void ClearAllDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
