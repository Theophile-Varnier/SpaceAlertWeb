
namespace SpaceAlert.Model.Site
{
    /// <summary>
    /// Membre du site
    /// </summary>
    public class Membre
    {
        public long Id { get; set; }

        public string Pseudo { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }
    }
}
