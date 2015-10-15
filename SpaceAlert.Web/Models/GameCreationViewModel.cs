
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SpaceAlert.Web.Models
{
    public class GameCreationViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is game owner.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is game owner; otherwise, <c>false</c>.
        /// </value>
        public bool IsGameOwner { get; set; }

        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        public GameViewModel Game { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public PlayerViewModel Player { get; set; }

        /// <summary>
        /// Gets or sets the name of the tuto.
        /// </summary>
        /// <value>
        /// The name of the tuto.
        /// </value>
        public string TutoName { get; set; }

        /// <summary>
        /// Gets or sets the available tutorials.
        /// </summary>
        /// <value>
        /// The available tutorials.
        /// </value>
        public IEnumerable<string> AvailableTutorials { get; set; }
    }
}