using SpaceAlert.DataAccess;
using SpaceAlert.DataAccess.Providers;
using SpaceAlert.DataAccess.Repositories;
using SpaceAlert.Model.Site;
using SpaceAlert.Model.Stats;
using SpaceAlert.Services.Exceptions;
using System.Collections.Generic;

namespace SpaceAlert.Business
{
    public class AccountService
    {

        private readonly SpaceAlertContext context;
        private readonly MembreProvider provider;

        public AccountService()
        {
            context = new SpaceAlertContext();
            provider = new MembreProvider(context);
        }

        /// <summary>
        /// Inscription d'un membre
        /// </summary>
        /// <param name="membre"></param>
        public void Inscrire(Membre membre)
        {
            provider.Add(membre);
        }

        /// <summary>
        /// Vérifie qu'un membre existe
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns></returns>
        public bool Existe(string pseudo)
        {
            return provider.GetMembreByPseudo(pseudo) != null;
        }

        public bool EmailDejaUtilise(string email)
        {
            return provider.GetMailIfExists(email) != null;
        }

        /// <summary>
        /// Ajoute un personnage à un membre
        /// </summary>
        public void AddCharacter(long membreId, string charName)
        {
            Personnage newPersonnage = new Personnage
            {
                MembreId = membreId,
                Nom = charName,
                Xp = 0
            };
            provider.AddCharacter(membreId, newPersonnage);
        }

        /// <summary>
        /// Tente de se connecter
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        public Membre RecupererMembre(string pseudo, string motDePasse)
        {
            Membre res;
            if (!Existe(pseudo))
            {
                throw new MembreNonExistantException(string.Format("Le pseudo {0} n'existe pas", pseudo));
            }
            res = provider.GetMembreByPseudoAndMdp(pseudo, motDePasse);
            if (res == null)
            {
                throw new MotDePasseInvalideException("Combinaison pseudo/mot de passe invalide");
            }
            return res;
        }

        /// <summary>
        /// Récupère un membre à l'aide de son pseudo
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns></returns>
        public Membre RecupererMembre(string pseudo)
        {
            return provider.GetMembreByPseudo(pseudo);
        }
    }
}
