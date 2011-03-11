using System;
using System.Collections.Generic;

namespace DominantSpecies {
  public class Map
  {
    static int MAP_WIDTH = 7;
    static int MAP_HEIGHT = 7;

    public Tile[,] tiles = new Tile[MAP_HEIGHT, MAP_WIDTH];

    // We assign the chits positions as though they were placed
    // at the top
    public Chit[,] chits = new Chit[MAP_HEIGHT + 1, MAP_WIDTH*2];

    public DataArrayWrapper<Tile> Tiles { get; set; }
    public DataArrayWrapper<Chit> Chits { get; set; }

    public Chit[] ChitsFor(int i, int j)
    {
      // We map chits to a double-width array
      j *= 2;
      return new Chit[] { chits[i,   j], chits[i, j+1],
                          chits[i+1, j-1], chits[i, j+2],
                          chits[i+1, j], chits[i+1, j+1] };
    }

    public void PlaceChit(int i, int j, Chit.Element e)
    {
      chits[i, j].element = e;
    }
    public void RemoveChit(int i, int j)
    {
      chits[i, j].element = Chit.Element.None;
    }

    public void PlaceTile(int i, int j, Tile.TerrainType t)
    {
      tiles[i, j].Terrain = t;
    }

    public void Glaciate(int i, int j)
    {
      tiles[i, j].tundra = true;
      for (int s = 0; s < tiles[i,j].Species.GetUpperBound(0); s++) {
        if (tiles[i,j].Species[s] > 1) 
          tiles[i, j].Species[s] = 1;
      }
    }

    public void Speciate(int i, int j, Player p)
    {
      var t = tiles[i, j];
      // TODO: ensure the player has enough, ask how many they want to do
      t.Species[(int) p.species] += t.speciateCount;
    }

    public Player DominatedBy(int i, int j) {
      return new Player(Species.Arachnid);
    }

    public Map()
    {
      for (int i = 0; i <= tiles.GetUpperBound(0); i++)
        for (int j = 0; j <= tiles.GetUpperBound(1); j++)
          tiles[i,j] = new Tile();

      Tiles = new DataArrayWrapper<Tile>(tiles);

      for (int i = 0; i <= chits.GetUpperBound(0); i++)
        for (int j = 0; j <= chits.GetUpperBound(1); j++)
          chits[i,j] = new Chit();

      Chits = new DataArrayWrapper<Chit>(chits);
    }
  }

  public class DataArrayWrapper<T>
  {
    Array data;

    public DataArrayWrapper(Array data)
    {
      this.data = data;
    }

    public T this[int i, int j]
    {
      get
      {
        return (T)data.GetValue(i, j);
      }
    }

    public List<T> All
    {
      get
      {
        List<T> list = new List<T>();
        for (int i = 0; i <= data.GetUpperBound(0); i++)
          for (int j = 0; j <= data.GetUpperBound(1); j++)
            list.Add((T)data.GetValue(i, j));

        return list;
      }
    }
  }

}
