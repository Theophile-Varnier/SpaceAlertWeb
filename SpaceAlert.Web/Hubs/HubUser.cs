
namespace SpaceAlert.Web.Hubs
{
    public class HubUser
    {
        public string GameId { get; set; }

        public string LastKnownConnectionId { get; set; }

        protected bool Equals(HubUser other)
        {
            return GameId.Equals(other.GameId) && string.Equals(LastKnownConnectionId, other.LastKnownConnectionId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((HubUser) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (GameId.GetHashCode() * 397) ^ (LastKnownConnectionId != null ? LastKnownConnectionId.GetHashCode() : 0);
            }
        }
    }
}