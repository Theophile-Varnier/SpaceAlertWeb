using SpaceAlert.Model.Jeu.Evenements;
using System;

namespace SpaceAlert.Business
{
    public delegate void NewEventEvent(object sender, NewEventArgs e);
    public class NewEventArgs : EventArgs
    {
        public Evenement Evenement { get; set; }
    }
}
