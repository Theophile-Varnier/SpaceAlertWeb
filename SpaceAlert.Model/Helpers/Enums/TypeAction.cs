
using System;
namespace SpaceAlert.Model.Helpers.Enums
{
    /// <summary>
    /// Les différentes actions possibles pour un joueur
    /// </summary>
    [Flags]
    public enum TypeAction
    {
        A = 0x01,
        B = 0x02,
        C = 0x04,
        Robots = 0x08
    }
}
