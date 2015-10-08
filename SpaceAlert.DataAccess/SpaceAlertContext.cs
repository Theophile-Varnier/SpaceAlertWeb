using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Plateau;
using SpaceAlert.Model.Site;
using SpaceAlert.Model.Stats;
using System.Data.Entity;

namespace SpaceAlert.DataAccess
{
    public class SpaceAlertContext : DbContext
    {
        public SpaceAlertContext()
            : base("SpaceAlert")
        {
            Database.SetInitializer(new SpaceAlertContextInitializer());
            Database.Initialize(true);
        }

        public DbSet<GameContext> GameContext { get; set; }

        public DbSet<Membre> Membres { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Joueur> Joueurs { get; set; }

        public DbSet<Campagne> Campagnes { get; set; }

        public DbSet<Personnage> Personnages { get; set; }

        public DbSet<MenaceInZone> MenacesInZones { get; set; }

        public DbSet<InGameMenace> Menaces { get; set; }

        public DbSet<ActionJoueur> Actions { get; set; }

        public DbSet<PersonnageInCampagne> PersonnagesInCampagnes { get; set; }

        public DbSet<ActionInTour> ActionsInTour { get; set; }

        public DbSet<InGameZone> Zones { get; set; }

        public DbSet<RampeInZone> RampesInZone { get; set; }

        public DbSet<Salle> Salles { get; set; }

        public DbSet<Vaisseau> Vaisseaux { get; set; }

        public DbSet<Statistiques> Statistiques { get; set; }

        public DbSet<PointsDeHublot> Hublots { get; set; }

        public DbSet<PartialDeck> Decks { get; set; }
    }
}
