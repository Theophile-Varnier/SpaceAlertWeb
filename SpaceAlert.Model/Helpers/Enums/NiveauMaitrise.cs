using System;

namespace SpaceAlert.Model.Helpers.Enums
{
    /// <summary>
    /// Niveau de maîtrise des actions spéciales
    /// </summary>
    [Flags]
    public enum NiveauMaitrise
    {
        NONE = 0,
        SIMPLE = 1,
        AVANCE = 2
    }
}
