using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[CreateAssetMenu(fileName = "New Rule Tile", menuName = "Tiles/Rule Tile")]
public class GameRuleTile : RuleTile
{
   public Color Color;
   public TileType Type;

   public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
   {
      base.GetTileData(position, tilemap, ref tileData);
      tileData.color = Color;
      tileData.flags = TileFlags.LockColor;
   }
}

[Serializable]
public enum TileType{
    Grass,
    Water,
    Sand,
    Lava,
    Wall
}
