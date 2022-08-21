using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PathFinder 
{
    class PathNode
    {
        public HexCell currentCell;
        public PathNode prevNode;
        public PathNode(HexCell cell, PathNode prev)
        {
            currentCell = cell;
            prevNode = prev;
        }
    }

    public static List<HexCell> TryFindPath(HexCell startCell, HexCell finishCell, int searchDeph)
    {
        List<HexCell> pathToPlayer = new List<HexCell>();

        List<PathNode> reachableNodes = new List<PathNode>();
        reachableNodes.Add(new PathNode(startCell, null));
        for (int i = 0; i < searchDeph; i++)
        {
            List<PathNode> nextLayerCells = GetNextLayerCells(reachableNodes);

            //ƒобавл€ем только незаблокированные €чейки
            nextLayerCells = nextLayerCells.Where(x =>  x.currentCell.IsFree() || 
                                                        x.currentCell == finishCell).ToList();

            reachableNodes.AddRange(nextLayerCells);

            if (IsCellInNodes(finishCell, reachableNodes))
            {
                //»щем ноду с €чейкой назначени€ и идЄм, по предыдущим к ней нодам, обратно
                PathNode currentNode = reachableNodes.Find(x => x.currentCell == finishCell);
                while(currentNode.prevNode != null)
                {
                    pathToPlayer.Add(currentNode.currentCell);
                    currentNode = currentNode.prevNode;
                }

                break;
            }
        }
        pathToPlayer.Reverse();
        return pathToPlayer;
    }

    private static List<PathNode> GetNextLayerCells(List<PathNode> reachedPathNodes)
    {
        List<PathNode> nextCellsLayer = new List<PathNode>();

        foreach (var node in reachedPathNodes)
        {
            List<PathNode> unvisitedCells = GetUnvisitedSurroundCells(node, reachedPathNodes);
            nextCellsLayer.AddRange(unvisitedCells);
        }

        return nextCellsLayer;
    }

    private static List<PathNode> GetUnvisitedSurroundCells(PathNode currentNode, List<PathNode> reachedPathNodes)
    {
        List<PathNode> unvisitedCells = new List<PathNode>();

        foreach(var cell in currentNode.currentCell.surroundingCells)
        {
            if(!IsCellInNodes(cell, reachedPathNodes))
            {
                unvisitedCells.Add(new PathNode(cell, currentNode));
            }
        }

        return unvisitedCells;
    }

    private static bool IsCellInNodes(HexCell cell, List<PathNode> reachedPathNodes)
    {
        foreach(var node in reachedPathNodes)
        {
            if (node.currentCell == cell)
                return true;
        }
        return false;
    }

}
