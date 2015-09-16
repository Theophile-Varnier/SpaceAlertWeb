using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une rampe
    /// </summary>
    public class Rampe
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the nb cases.
        /// </summary>
        /// <value>
        /// The nb cases.
        /// </value>
        public int NbCases { get; set; }

        /// <summary>
        /// Gets or sets the special cases.
        /// </summary>
        /// <value>
        /// The special cases.
        /// </value>
        public Dictionary<TypeCase, List<int>> SpecialCases { get; set; }
    }
}
