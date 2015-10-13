using System;

namespace SpaceAlert.Model.Helpers.Enums
{
    /// <summary>
    /// Niveau de maîtrise des actions spéciales
    /// </summary>
    [Flags]
    public enum NiveauMaitrise
    {
        None = 0x00,
        Simple = 0x01,
        Avance = 0x02
    }
}
