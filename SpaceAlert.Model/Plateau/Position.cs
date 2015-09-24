using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Model.Plateau
{
    public class Position
    {
        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>
        /// The zone.
        /// </value>
        public Zone Zone { get; set; }

        /// <summary>
        /// Gets or sets the pont.
        /// </summary>
        /// <value>
        /// The pont.
        /// </value>
        public Pont Pont { get; set; }

        public Position(Zone z, Pont p)
        {
            Zone = z;
            Pont = p;
        }

        public Position()
        {

        }
    }
}