using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Helpers
{
    public enum EventType
    {
        MENACE = 0,
        MENACE_SERIEUSE,
        MENACE_INTERNE,
        MENACE_INTERNE_SERIEUSE,
        DONNEES_ENTANTES,
        TRANSFERT_DONNEES,
        COUPURE_COMMUNICATION,
        FIN_PHASE
    }
}
