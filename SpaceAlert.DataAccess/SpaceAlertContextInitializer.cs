using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.DataAccess
{
    public class SpaceAlertContextInitializer : DropCreateDatabaseIfModelChanges<SpaceAlertContext>
    {
        protected override void Seed(SpaceAlertContext context)
        {
            base.Seed(context);
        }
    }
}
