using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;
using System.Linq;
using System.Timers;

namespace SpaceAlert.Business
{
    /// <summary>
    /// Classe à utiliser côté IHM pour mettre la vue à jour lors de la réception d'événements
    /// </summary>
    public class GameExecutionManager
    {
        public Game Game { get; private set; }

        public event NewEventEvent NewEventEvent;

        private Evenement nextEvent;

        private Timer timer;

        /// <summary>
        /// Constructeur
        /// </summary>
        public GameExecutionManager(Game game)
        {
            Game = game;
            nextEvent = game.Mission.Evenements.OrderBy(e => e.Annonce).First();
            timer = new Timer
            {
                Interval = nextEvent.Annonce.TotalMilliseconds
            };
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (NewEventEvent != null)
            {
                NewEventEvent(this, new NewEventArgs
                {
                    Evenement = nextEvent
                });
            }
            Evenement futurEvent = Game.Mission.Evenements.OrderBy(ev => ev.Annonce).FirstOrDefault(ev => ev.Annonce > nextEvent.Annonce);
            if (futurEvent != null)
            {
                timer.Interval = futurEvent.Annonce.TotalMilliseconds - nextEvent.Annonce.TotalMilliseconds;
                nextEvent = futurEvent;
            }
            else
            {
                timer.Enabled = false;
                timer.Elapsed -= timer_Elapsed;
            }
        }
    }
}
