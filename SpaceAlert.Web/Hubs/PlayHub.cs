
namespace SpaceAlert.Web.Hubs
{
    public class PlayHub : AbstractHub
    {

        /// <summary>
        /// Pops the menace.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="message">The message.</param>
        public void PopMenace(string gameId, string frontImage, string backImage)
        {
            Clients.Group(gameId).popMenace(frontImage, backImage);
        }

        /// <summary>
        /// Ends the phase.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="phase">The phase.</param>
        public void FinDePhase(string gameId, int phase)
        {
            Clients.Group(gameId).endPhase(phase);
        }
    }
}