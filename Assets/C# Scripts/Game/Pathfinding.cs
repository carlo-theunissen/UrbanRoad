using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game
{
	public class Pathfinding
	{

	    private static Pathfinding instance = null;
	    private Vector2 left = new Vector2(-1, 0);//0
	    private Vector2 up = new Vector2(0, 1);//1
	    private Vector2 right = new Vector2(1, 0);//2
	    private Vector2 down = new Vector2(0, -1);//3


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
	        if (level.getPos(start) != null || level.getPos(end) != null) {
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
			RoadPiece[] pieces = new RoadPiece[previous.Count];
			int index = 0;
			foreach (Vector2 vec in previous) {
				pieces [index] = new RoadPiece ();
				pieces [index].Position = vec;
				index++;
			}
            int lastAction = 1;
            index = 0;
            foreach (RoadPiece current in pieces) {
                
                int nextAction = 1;
                if (index + 1 < pieces.Length)
                {
                    Vector2 diff = current.Position - pieces[index + 1].Position;
                    if (diff.Equals(left)) {
                        //the diffrence from left
                        nextAction = 2;
                    }
                    if (diff.Equals(up))
                    {
                        nextAction = 1;
                    }
                    if (diff.Equals(right)) {
                        //the diffrence from right
                        nextAction = 0;
                    }
                    if (diff.Equals(down)) {
                        nextAction = 3;
                    }
                }

                pieces[index ++].type = calculateRoadType(lastAction, nextAction);
                switch(lastAction) {
                    case 0:
                        current.flipFrom = Direction.LEFT;
                        break;
                    case 1:
                        current.flipFrom = Direction.UP;
                        break;
                    case 2:
                        current.flipFrom = Direction.RIGHT;
                        break;
                    case 3:
                        current.flipFrom = Direction.BOTTOM;
                        break;
                }
                lastAction = nextAction;
            }
			return pieces; 
	    } 

        private int calculateRoadType(int lastAction, int nextAction) {
            // from left to ...
            if (lastAction == 0 && nextAction == 0)
            {
                return 2;
            }
            if (lastAction == 0 && nextAction == 1)
            {
                return 5;
            }
            if (lastAction == 0 && nextAction == 3)
            {
                return 3;
            }
            // from up to ...
            if (lastAction == 1 && nextAction == 0)
            {
                return 6;
            }
            if (lastAction == 1 && nextAction == 1)
            {
                return 1;
            }
            if (lastAction == 1 && nextAction == 2) 
            {
                return 3;
            }
            // from right to ...
            if (lastAction == 2 && nextAction == 1) {
                return 4;
            }
            if (lastAction == 2 && nextAction == 2) {
                return 2;
            }
            if (lastAction == 2 && nextAction == 3) {
                return 6;
            }
            // from down to ..
            if (lastAction == 3 && nextAction == 0) {
                return 4;
            }
            if (lastAction == 3 && nextAction == 2) {
                return 5;
            }
            if (lastAction == 3 && nextAction == 3) {
                return 1;
            }

            return 1;
        }

	    private Vector2[] getNeighbors(Level level,Vector2 pos, ref List<Vector2> previous) {
	        List<Vector2> array = new List<Vector2>();
	        Vector2[] checks = { pos + left, pos + up, pos + right, pos + down };
	        foreach(Vector2 check in checks) { 
				if(isValidPos((int) check.x, (int) check.y, ref level) && level.getPos(check) == null && !search(check,ref previous)) {
	                array.Add(check);
	            }
	        }
	        return array.ToArray();
	    }

		private bool isValidPos(int x, int y, ref Level level){
			return x < level.getWidth () && y < level.getHeight () && x >= 0 && y >= 0 ;
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
}