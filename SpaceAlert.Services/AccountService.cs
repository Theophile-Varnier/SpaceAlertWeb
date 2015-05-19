using SpaceAlert.DataAccess.Repositories;
using SpaceAlert.Model.Site;
using SpaceAlert.Services.Exceptions;

namespace SpaceAlert.Services
{
    public class AccountService
    {
        private readonly MembreRepository membreRepository = new MembreRepository();

        /// <summary>
        /// Inscription d'un membre
        /// </summary>
        /// <param name="membre"></param>
        public void Inscrire(Membre membre)
        {
            membreRepository.EnregistrerMembre(membre);
        }

        /// <summary>
        /// Vérifie qu'un membre existe
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns></returns>
        public bool Existe(string pseudo)
        {
            return membreRepository.GetExistingMember(pseudo) != null;
        }

        public bool EmailDejaUtilise(string email)
        {
            return membreRepository.GetExistingEmail(email) != null;
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
            if ((res = membreRepository.GetExistingMember(pseudo, motDePasse)) == null)
            {
                throw new MotDePasseInvalideException("Combinaison pseudo/mot de passe invalide");
            }
            return res;
        }
    }
}
