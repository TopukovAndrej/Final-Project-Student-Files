namespace StudentFiles.Domain.Common.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        public Guid Uid { get; protected set; }

        public bool IsDeleted { get; protected set; }

        protected Entity(int id, Guid uid, bool isDeleted)
        {
            Id = id;
            Uid = uid;
            IsDeleted = isDeleted;
        }
    }
}
