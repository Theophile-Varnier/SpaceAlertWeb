using System;

namespace SpaceAlert.Model.Helpers.Enums
{
    /// <summary>
    /// Niveau de maîtrise des actions spéciales
    /// </summary>
    [Flags]
    public enum NiveauMaitrise
    {
        None = 0,
        Simple = 1,
        Avance = 2
    }
}
