using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Services
{
    public class ServiceProvider
    {
        private AccountService accountService;

        public AccountService AccountService
        {
            get { return accountService ?? (accountService = new AccountService()); }
        }
    }
}
