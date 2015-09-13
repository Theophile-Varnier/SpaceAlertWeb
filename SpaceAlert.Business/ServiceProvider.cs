using SpaceAlert.DataAccess;
namespace SpaceAlert.Business
{
    /// <summary>
    /// Donne accès à des singletons des différents services
    /// </summary>
    public class ServiceProvider
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        private AccountService accountService;

        private GameService gameService;

        public AccountService AccountService
        {
            get { return accountService ?? (accountService = new AccountService(unitOfWork)); }
        }

        public GameService GameService
        {
            get { return gameService ?? (gameService = new GameService(unitOfWork)); }
        }

        /// <summary>
        /// Initialise le contexte du jeu
        /// </summary>
        public static void Init()
        {
            SpaceAlertData.Init();
        }
    }
}
