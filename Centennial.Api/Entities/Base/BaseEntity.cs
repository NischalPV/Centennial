using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Centennial.Api.Entities
{

    public record IBaseEntity
    {
        
        protected List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();


        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

    }

    public abstract record BaseEntity<T>: IBaseEntity
    {
        int? _requestedHashCode;
        T _Id;
        public virtual T Id { get { return _Id; } protected set { _Id = value; } }
        public DateTime CreatedDate { get; protected set; }
        public bool IsActive { get; set; }

        public bool IsTransient()
        {
            return EqualityComparer<T>.Default.Equals(this.Id, default(T));
            // return Id == default(T);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }

        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
