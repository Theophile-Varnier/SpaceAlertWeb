﻿
namespace SpaceAlert.Web.Models
{
    public class GameCreationViewModel
    {
        public string CreatedBy { get; set; }

        public bool IsGameOwner { get; set; }

        public GameViewModel Game { get; set; }
    }
}