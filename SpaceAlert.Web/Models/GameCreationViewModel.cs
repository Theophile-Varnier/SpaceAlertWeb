
namespace SpaceAlert.Web.Models
{
    public class GameCreationViewModel
    {
        public string CreatedBy { get; set; }

        public string CreatorConnectionId { get; set; }

        public GameViewModel Game { get; set; }
    }
}