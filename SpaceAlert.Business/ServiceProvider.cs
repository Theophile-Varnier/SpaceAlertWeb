namespace SpaceAlert.Business
{
    /// <summary>
    /// Donne accès à des singletons des différents services
    /// </summary>
    public class ServiceProvider
    {
        private AccountService accountService;

        private GameService gameService;

        public AccountService AccountService
        {
            get { return accountService ?? (accountService = new AccountService()); }
        }

        public GameService GameService
        {
            get { return gameService ?? (gameService = new GameService()); }
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
