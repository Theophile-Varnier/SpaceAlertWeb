using System;
namespace SpaceAlert.Model.Helpers.Enums
{
    [Flags]
    public enum TypeMenace
    {
        MenaceExterne = 0x01,
        MenaceExterneSerieuse = 0x02,
        MenaceInterne = 0x04,
        MenaceInterneSerieuse = 0x08
    }
}
