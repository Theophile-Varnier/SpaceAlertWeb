
using System;
namespace SpaceAlert.Model.Helpers.Enums
{
    [Flags]
    public enum ActionC
    {
        Robots = 0x01,
        Roquettes = 0x02,
        Maintenance,
        Hublot,
        Intercepteurs
    }
}
