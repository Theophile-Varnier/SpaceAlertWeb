using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Site;
using SpaceAlert.Model.Stats;
using System.Data.Entity;

namespace SpaceAlert.DataAccess
{
    public class SpaceAlertContext: DbContext
    {
        public SpaceAlertContext()
            : base("SpaceAlert")
        {
            Database.SetInitializer<SpaceAlertContext>(new SpaceAlertContextInitializer());
            Database.Initialize(true);
        }

        public DbSet<Membre> Membres { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Joueur> Joueurs { get; set; }

        public DbSet<Campagne> Campagnes { get; set; }

        public DbSet<Personnage> Personnages { get; set; }
    }
}
