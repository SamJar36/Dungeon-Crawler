using Dungeon_Crawler.Elements;
using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler;

public class LevelElementJsonObject
{
    public List<Wall> Walls { get; set; }
    public List<LockedDoor> LockedDoors { get; set; }

    public LevelElementJsonObject(List<LevelElement> levelElementList)
    {
        this.Walls = new List<Wall>();
        this.LockedDoors = new List<LockedDoor>();
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
        }
    }
    public LevelElementJsonObject()
    {

    }
    public List<LevelElement> CombineLists()
    {
        List<LevelElement> levelElementList = new List<LevelElement>();
        return levelElementList.Concat(Walls).Concat(LockedDoors).ToList();
    }
}
