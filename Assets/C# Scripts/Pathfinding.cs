using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding
{

    private static Pathfinding instance = null;
    private Vector2 left = new Vector2(-1, 0);
    private Vector2 up = new Vector2(0, 1);
    private Vector2 right = new Vector2(1, 0);
    private Vector2 down = new Vector2(0, -1);
    private Vector2[] prev;

    private Pathfinding()
    { }


    public static Pathfinding getInstance()
    {
        if (instance == null)
        {
            instance = new Pathfinding();
        }
        return instance;
    }

    public RoadPiece[] getRoad(Level level)
    {
        Vector2 start = level.getStart();
        Vector2 end = level.getFinish();
        if (level.getPos(start) != null || level.getPos(level.getFinish()) != null) {
            return null;
        }
        List<Vector2> previous = new List<Vector2>();
        while (true) {
            previous.Add(start);
            if (start.x == end.x && start.y == end.y) {
                return makeRoadPieces(ref previous);
            }
            Vector2[] neighbors = getNeighbors(level, start, ref previous);
            if (neighbors.Length != 1) {
                return null;
            }
            start = neighbors[0];
        }
    }
    private RoadPiece[] makeRoadPieces(ref List<Vector2> previous) {
        //TODO
    }
    private Vector2[] getNeighbors(Level level,Vector2 pos, ref List<Vector2> previous) {
        List<Vector2> array = new List<Vector2>();
        Vector2[] checks = { pos + left, pos + up, pos + right, pos + down };
        foreach(Vector2 check in checks) { 
            if(level.getPos(check) == null && !search(check,ref previous)) {
                array.Add(check);
            }
        }
        return array.ToArray();
    }
    private bool search(Vector2 pos, ref List<Vector2> previous) {
        foreach(Vector2 temp in previous) {
            if(temp.x == pos.x && temp.y == pos.y) {
                return true;
            }
        }
        return false;
    }

}
