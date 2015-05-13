
namespace SpaceAlert.Model.Menaces
{
    /// <summary>
    /// Décrit les menaces internes
    /// </summary>
    public class MenaceInterne: Menace
    {
        /// <summary>
        /// Indique si la menace riposte à une attaque de robot
        /// </summary>
        public bool Riposte { get; set; }
    }
}
