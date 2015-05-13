
namespace SpaceAlert.Model.Menaces
{
    /// <summary>
    /// Décrit les menaces externes
    /// </summary>
    public class MenaceExterne : Menace
    {
        /// <summary>
        /// Indique si la menace est ciblable par les canons
        /// </summary>
        public bool Targetable { get; set; }

        /// <summary>
        /// Indique si la menace est ciblable par les roquettes
        /// </summary>
        public bool RocketTargetable { get; set; }
    }
}
