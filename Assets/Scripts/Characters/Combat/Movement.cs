using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using Assets.Scripts.Characters.Combat;

public class Movement : MonoBehaviour
{
    public List<List<GameObject>> routes;
    public List<GameObject> selectedRoute;
    public GameObject hoveredTile;
    public int currentRouteInd;

    public void Pull(IMovable moveToward, int pullAmount, GameObject objectToPull)
    {
        var fullRoute = objectToPull.GetComponent<Enemy>().GetPosition().GetComponent<Tile>().FindShortestPathsTo(moveToward.GetPosition());
        var pullPos = fullRoute[0][pullAmount];
        objectToPull.GetComponent<Movement>().MoveToTile(pullPos, objectToPull.GetComponent<Enemy>());
    }

    public void MoveToSelectedTile(IMovable movable)
    {
        var stats = gameObject.GetComponent<Stats>();
        if(stats.curMovement < selectedRoute.Count - 1)
        {
            Debug.Log("Not enough movement left");
            return;
        }

        var to = selectedRoute.Last();
        if (!to.GetComponent<Tile>().IsObstructed())
        {
            MoveToTile(to, movable);
            stats.DecreaseMovement(selectedRoute.Count - 1);
        }
    }

    public void MoveToTile(GameObject tile, IMovable movable)
    {
        gameObject.transform.position = new Vector3(tile.transform.position.x, 2.1f, tile.transform.position.z);
        movable.SetPosition(tile);
    }

    public Dictionary<GameObject, int> GetDistanceToPlayers(IMovable movable, List<GameObject> players)
    {
        var distances = new Dictionary<GameObject, int>();
        foreach (GameObject player in players)
        {
            var playerPos = player.GetComponent<Player>().GetPosition();
            distances.Add(player, movable.GetPosition().GetComponent<Tile>().FindShortestPathsTo(playerPos)[0].Count-1);
        }
        return distances;
    }

    public void MoveToClosestPlayer(IMovable movable, List<GameObject> players)
    {
        var distances = GetDistanceToPlayers(movable, players);
        int lowestValue = distances.Min(kvp => kvp.Value);
        List<GameObject> closestPlayers = distances
            .Where(kvp => kvp.Value == lowestValue)
            .Select(kvp => kvp.Key)
            .ToList();
        var route = movable.GetPosition().GetComponent<Tile>().FindShortestPathsTo(closestPlayers[0].GetComponent<Player>().GetPosition())[0];
       
        if (route.Count > 0)
        {
            route.RemoveAt(route.Count - 1);
            selectedRoute = route;
            MoveToSelectedTile(movable);
        }
    }

    // Hover over tiles
    public void HoverOverTile(IMovable movable, GameObject hoveredObject, List<GameObject> tiles)
    {
        if (hoveredTile != null && hoveredTile == hoveredObject)
        {
            return;
        }
        else
        {
            routes = movable.GetPosition().GetComponent<Tile>().FindShortestPathsTo(hoveredObject);
            
            if (gameObject.GetComponent<Stats>().curMovement >= routes[0].Count - 1)
            {
                hoveredTile = hoveredObject;
                selectedRoute = routes[0];
                currentRouteInd = 0;
                OutlineTiles(tiles);
            }
        }
    }

    public void SelectRoute(int ind)
    {
        selectedRoute = routes[ind];
        currentRouteInd = ind;
    }

    public void NextRoute(List<GameObject> tiles)
    {
        var nextInd = 0;
        if (currentRouteInd != routes.Count-1)
        {
            nextInd = currentRouteInd + 1;
        }
        SelectRoute(nextInd);
        OutlineTiles(tiles);
    }

    public void PreviousRoute(List<GameObject> tiles)
    {
        var nextInd = routes.Count-1;
        if (currentRouteInd != 0)
        {
            nextInd = currentRouteInd - 1;
        }
        SelectRoute(nextInd);
        OutlineTiles(tiles);
    }

    public void OutlineTiles(List<GameObject> tiles)
    {
        foreach (GameObject t in tiles)
        {
            t.GetComponent<Tile>().RemoveOutlineShader();
        }

        foreach (GameObject t in selectedRoute)
        {
            t.GetComponent<Tile>().AddOutlineShader();
        }
    }

    public void ResetTileHovering(List<GameObject> tiles)
    {
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<Tile>().RemoveOutlineShader();
        }
        routes = null;
        selectedRoute = null;
        hoveredTile = null;
    }
}
