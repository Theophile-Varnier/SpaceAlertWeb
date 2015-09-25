
namespace SpaceAlert.Model.Helpers.Enums
{
    /// <summary>
    /// Les directions possibles
    /// /!\ Ne pas utiliser d'opérations bit à bit
    /// à cause de la valeur négative du mouvement vers la gauche
    /// </summary>
    public enum Direction
    {
        Blue = -1,
        Asc = 0,
        Red = 1
    }
}
