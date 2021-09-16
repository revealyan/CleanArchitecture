namespace Revealyan.CleanArchitecture.Domain
{

    public abstract class Entity
    {
        private int? _requestedHashCode;
        private int _Id;
        public virtual int Id 
        {
            get => _Id;
            set => _Id = value;
        }

        public bool IsTransient() => Id == default;

        public override bool Equals(object obj)
        {
            if (obj is not Entity item ||
                GetType() != obj.GetType() ||
                item.IsTransient() || 
                IsTransient()
                )
                return false;
            return item.Id == Id;
        }
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    // XOR for random distribution (https://ericlippert.com/2011/02/28/guidelines-and-rules-for-gethashcode/)
                    _requestedHashCode = Id.GetHashCode() ^ 31; 

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (left is not null &&
                right is not null)
                return left.Equals(right);
            else
                return false;
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
