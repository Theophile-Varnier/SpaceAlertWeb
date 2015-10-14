using SpaceAlert.Model.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Jeu
{
    public class GameConfig
    {
        /// <summary>
        /// Gets or sets the allowed actions.
        /// </summary>
        /// <value>
        /// The allowed actions.
        /// </value>
        public TypeAction AllowedActions { get; set; }

        /// <summary>
        /// Gets or sets the allowed actions c.
        /// </summary>
        /// <value>
        /// The allowed actions c.
        /// </value>
        public ActionC AllowedActionsC { get; set; }

        /// <summary>
        /// Gets or sets the allowed threats.
        /// </summary>
        /// <value>
        /// The allowed threats.
        /// </value>
        public TypeMenace AllowedThreats { get; set; }

        /// <summary>
        /// Gets or sets the special actions.
        /// </summary>
        /// <value>
        /// The special actions.
        /// </value>
        public SpecialAction SpecialActions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ship damages].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ship damages]; otherwise, <c>false</c>.
        /// </value>
        public bool ShipDamages { get; set; }

        /// <summary>
        /// Gets or sets the cards per phase.
        /// </summary>
        /// <value>
        /// The cards per phase.
        /// </value>
        public Dictionary<int, int> CardsPerPhase { get; set; }
    }
}
