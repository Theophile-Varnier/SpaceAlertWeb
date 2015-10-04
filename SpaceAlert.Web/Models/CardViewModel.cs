using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Web.Models
{
    public class CardViewModel
    {
        public Direction Direction { get; set; }

        public TypeAction Type { get; set; }

        public string FrontImgUri { get; set; }

        public string BackImageUri { get; set; }
    }
}