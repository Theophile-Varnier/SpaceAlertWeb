using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Web.Models
{
    public class CardViewModel
    {
        public int Direction { get; set; }

        public int Type { get; set; }

        public string FrontImgUri { get; set; }

        public string BackImageUri { get; set; }
    }
}