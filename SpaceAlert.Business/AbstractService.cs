using SpaceAlert.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Business
{
    public class AbstractService
    {
        protected readonly UnitOfWork unitOfWork;

        public AbstractService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
