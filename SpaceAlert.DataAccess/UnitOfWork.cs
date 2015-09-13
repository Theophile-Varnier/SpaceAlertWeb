using SpaceAlert.DataAccess.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.DataAccess
{
    public class UnitOfWork
    {
        public UnitOfWork()
        {
            Context = new SpaceAlertContext();
            GameProvider = new GameProvider(Context);
            GameContextProvider = new GameContextProvider(Context);
            MembreProvider = new MembreProvider(Context);
            PersonnageProvider = new PersonnageProvider(Context);
            JoueurProvider = new JoueurProvider(Context);
        }

        public SpaceAlertContext Context { get; private set; }

        public GameProvider GameProvider { get; private set; }

        public MembreProvider MembreProvider { get; private set; }

        public PersonnageProvider PersonnageProvider { get; private set; }

        public JoueurProvider JoueurProvider { get; private set; }

        public GameContextProvider GameContextProvider { get; private set; }
    }
}
