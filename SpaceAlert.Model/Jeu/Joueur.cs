
namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente un joueur d'une partie
    /// </summary>
    public class Joueur
    {
        public string NomPersonnage { get; set; }

        public string Couleur { get; set; }

        public bool IsCapitaine { get; set; }

        public long MembreId { get; set; }
    }
}
