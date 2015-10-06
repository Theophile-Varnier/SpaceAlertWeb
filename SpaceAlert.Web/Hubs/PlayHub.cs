
using System.Threading.Tasks;
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
        /// Transferts the card.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="newMembre">The new membre.</param>
        /// <param name="cardDirection">The card direction.</param>
        /// <param name="cardAction">The card action.</param>
        /// <returns></returns>
        public async Task TransfertCard(string gameId, string newMembre, int cardDirection, int cardAction)
        {
            Clients.OthersInGroup(gameId).receiveNewCard(newMembre, cardDirection, cardAction);
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