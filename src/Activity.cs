using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  public enum ActivityType
  {
    /* Activity types for actions on the ActionDisplay */
    Abundance,
    
    /* Planning phase activities */
    PlaceActionPawn
  }
  
  public abstract class Activity
  {
    public Activity ()
    {
    }
    
    public abstract bool IsValid { get; }
    
    public abstract void Do(GameController GC);
    
    public abstract void Undo(GameController GC);
    
    public abstract ActivityType Type { get; }
  }
    
  public class AbundanceActivity : Activity
  {
    public List<Chit.ElementType> ValidTypes { get; private set; }
    public List<Chit> ValidChits { get; private set; }
    public Player Player { get; private set; }
    
    public Chit SelectedChit { get; set; }
    public Chit.ElementType SelectedElementType { get; set; }
    
    public AbundanceActivity(Player player, Chit.ElementType[] validTypes, Chit[] validChits) : this(player,
                                                                                                     new List<Chit.ElementType>(validTypes),
                                                                                                     new List<Chit>(validChits))
    {
    }
    
    public AbundanceActivity(Player player, List<Chit.ElementType> validTypes, List<Chit> validChits)
    {
      ValidTypes = validTypes;
      ValidChits = validChits;
      Player = player;
      
      SelectedElementType = Chit.ElementType.None;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Abundance; }
    }
    
    public override bool IsValid
    {
      get
      {
        if (SelectedChit == null || SelectedElementType == Chit.ElementType.None)
        {
          return false;
        }
        
        return true;
      }
    }
    
    public override void Do(GameController GC)
    {
      GC.PlaceChit(SelectedChit, SelectedElementType);
    }
    
    public override void Undo(GameController GC)
    {
      GC.RemoveChit(SelectedChit);
    }
  }
  
  public class PlaceActionPawnActivity : Activity
  {
    public ActivityType SelectedAction { get; set; }
    public Player CurrentPlayer { get; set; }
    
    public override ActivityType Type {
      get { return ActivityType.PlaceActionPawn; }
    }
    
    public override bool IsValid {
      get {
        if (CurrentPlayer == null)
          return false;
        
        return true;
      }
    }
    
    public override void Do (GameController GC)
    {
      GC.PlaceActionPawn(CurrentPlayer, SelectedAction);
    }
    
    public override void Undo (GameController GC)
    {
      throw new NotImplementedException ();
    }
  }
}