using SpaceAlert.Model.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceAlert.Web.Models
{
    public class ActionViewModel
    {
        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        public GenreAction Genre { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the tour.
        /// </summary>
        /// <value>
        /// The tour.
        /// </value>
        public int Tour { get; set; }
    }
}