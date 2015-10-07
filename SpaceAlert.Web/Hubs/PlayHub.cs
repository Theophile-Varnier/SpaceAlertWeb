
using SpaceAlert.Model.Helpers.Enums;
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
        public async Task PopMenace(string gameId, string frontImage, string backImage)
        {
            Clients.Group(gameId).popMenace(frontImage, backImage);
        }

        /// <summary>
        /// Datas the transfert.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        public async Task DataTransfert(string gameId)
        {
            Clients.Group(gameId).enableDataTransfert();
        }

        /// <summary>
        /// Transferts the card.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="newMembre">The new membre.</param>
        /// <param name="cardDirection">The card direction.</param>
        /// <param name="cardAction">The card action.</param>
        /// <returns></returns>
        public async Task TransfertCard(string gameId, string newMembre, Direction cardDirection, TypeAction cardAction)
        {
            Clients.Group(gameId).receiveNewCard(newMembre, cardDirection, cardAction);
        }

        /// <summary>
        /// Ends the phase.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="phase">The phase.</param>
        public async Task FinDePhase(string gameId, int phase)
        {
            Clients.Group(gameId).endPhase(phase);
        }
    }
}