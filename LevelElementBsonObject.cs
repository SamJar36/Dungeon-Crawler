using Dungeon_Crawler.Elements;
using DungeonCrawler;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler;

public class LevelElementBsonObject
{
    public List<Wall> Walls { get; set; }
    public List<LockedDoor> LockedDoors { get; set; }
    public List<ArrowTile> ArrowTiles { get; set; }
    public List<FakeWall> FakeWalls { get; set; }
    public List<FinishLevel> FinishLevels { get; set; }
    public List<Gold> Golds { get; set; }
    public List<GreenWall> GreenWalls { get; set; }
    public List<HeartPiece> HeartPieces { get; set; }
    public List<HiddenWall> HiddenWalls { get; set; }
    public List<Key> Keys { get; set; }
    public List<MagicalBarrier> MagicalBarriers { get; set; }
    public List<MagicalKey> MagicalKeys { get; set; }
    public List<PushableBlock> PushableBlocks { get; set; }
    public List<ShopKeeper> ShopKeepers { get; set; }
    public List<TreasureChest> TreasureChests { get; set; }
    public List<Warp> Warps { get; set; }

    public LevelElementBsonObject(List<LevelElement> levelElementList)
    {
        this.Walls = new List<Wall>();
        this.LockedDoors = new List<LockedDoor>();
        this.ArrowTiles = new List<ArrowTile>();
        this.FakeWalls = new List<FakeWall>();
        this.FinishLevels = new List<FinishLevel>();
        this.Golds = new List<Gold>();
        this.GreenWalls = new List<GreenWall>();
        this.HeartPieces = new List<HeartPiece>();
        this.HiddenWalls = new List<HiddenWall>();
        this.Keys = new List<Key>();
        this.MagicalBarriers = new List<MagicalBarrier>();
        this.MagicalKeys = new List<MagicalKey>();
        this.PushableBlocks = new List<PushableBlock>();
        this.ShopKeepers = new List<ShopKeeper>();
        this.TreasureChests = new List<TreasureChest>();
        this.Warps = new List<Warp>();
        foreach (var element in levelElementList)
        {
            if (element is Wall wall)
            {
                Walls.Add(wall);
            }
            else if (element is LockedDoor ldoor)
            {
                LockedDoors.Add(ldoor);
            }
            else if (element is ArrowTile arrow)
            {
                ArrowTiles.Add(arrow);
            }
            else if (element is FakeWall fwall)
            {
                FakeWalls.Add(fwall);
            }
            else if (element is FinishLevel finish)
            {
                FinishLevels.Add(finish);
            }
            else if (element is Gold gold)
            {
                Golds.Add(gold);
            }
            else if (element is GreenWall gwall)
            {
                GreenWalls.Add(gwall);
            }
            else if (element is HeartPiece heart)
            {
                HeartPieces.Add(heart); 
            }
            else if (element is HiddenWall hwall)
            {
                HiddenWalls.Add(hwall);
            }
            else if (element is Key key)
            {
                Keys.Add(key);
            }
            else if (element is MagicalBarrier magbarr)
            {
                MagicalBarriers.Add(magbarr);
            }
            else if (element is MagicalKey magkey)
            {
                MagicalKeys.Add(magkey);
            }
            else if (element is PushableBlock pblock)
            {
                PushableBlocks.Add(pblock);
            }
            else if (element is ShopKeeper shop)
            {
                ShopKeepers.Add(shop);
            }
            else if (element is TreasureChest chest)
            {
                TreasureChests.Add(chest);
            }
            else if (element is Warp warp)
            {
                Warps.Add(warp);
            }
        }
    }
    public LevelElementBsonObject()
    {

    }
    public List<LevelElement> CombineLists()
    {
        List<LevelElement> levelElementList = new List<LevelElement>();
        return levelElementList
            .Concat(Walls)
            .Concat(LockedDoors)
            .Concat(ArrowTiles)
            .Concat(FakeWalls)
            .Concat(FinishLevels)
            .Concat(Golds)
            .Concat(GreenWalls)
            .Concat(HeartPieces)
            .Concat(HiddenWalls)
            .Concat(Keys)
            .Concat(MagicalBarriers)
            .Concat(MagicalKeys)
            .Concat(PushableBlocks)
            .Concat(ShopKeepers)
            .Concat(TreasureChests)
            .Concat(Warps)
            .ToList();
    }
}
