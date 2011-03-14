using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class PlaceActionPawnActivity : PlayerActivity
  {
    public PlaceActionPawnActivity(Player player) : base(player)
    {
    }
    
    public ActionType SelectedAction { get; set; }
    
    public override ActivityType Type {
      get { return ActivityType.PlaceActionPawn; }
    }
    
    public override bool IsValid {
      get {
        if (Player == null)
          return false;
        
        return true;
      }
    }
    
    public override void Do (GameController GC)
    {
      GC.PlaceActionPawn(Player, SelectedAction);
    }
    
    public override void Undo (GameController GC)
    {
      throw new NotImplementedException ();
    }
  }
}