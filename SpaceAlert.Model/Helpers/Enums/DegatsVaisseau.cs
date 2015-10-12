using System;

namespace SpaceAlert.Model.Helpers.Enums
{
    [Flags]
    public enum DegatsVaisseau
    {
        None = 0x00,
        CanonLourd = 0x01,
        CanonLeger = 0x02,
        Ascenseur = 0x04,
        Blindage = 0x08,
        Boucliers = 0x10,
        Energie = 0x20,
        All = CanonLourd | CanonLeger | Ascenseur | Blindage | Boucliers | Energie
    }
}