using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;


public class Tile : MonoBehaviour
{

    public enum Type
    {
        water,
        tar,
        grass,
        highGrass,
        deepWater,
        sand,
        plain
    }

    public GameObject parent;
    public GameObject characterOnTile;
    public List<GameObject> tokens;
    public List<GameObject> connectedTiles = new() { };
    public Type type;
    public GameObject gasPrefab;
    public GameObject treePrefab;
    public Shader outlineShader;
    public Material originalMaterial;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
        }
    }

    public List<GameObject> TilesInAoe(int area)
    {
        List<GameObject> tilesInRange = new() { parent };
        List<List<GameObject>> queue = new() { tilesInRange };

        for (int i = 0; i < area; i++)
        {
            foreach (GameObject tile in queue[i])
            {
                List<GameObject> adjacents = tile.GetComponent<Tile>().connectedTiles;
                foreach (GameObject adj in adjacents)
                {
                    tilesInRange.Add(adj);
                }
                queue.Add(adjacents);
            }
        }
        RemoveDuplicates(tilesInRange);
        tilesInRange.Remove(parent);
        return tilesInRange;
    }

    // public List<GameObject> TilesInCone(GameObject target, int range){ }

    public List<GameObject> GetTargetsInAoe(AttackData.Target targets, int area)
    {
        var tilesInAoe = TilesInAoe(area);
        var targetsInAoe = new List<GameObject>() { };
        
        foreach (GameObject tile in tilesInAoe)
        {
            var charOnTile = tile.GetComponent<Tile>().characterOnTile;

            if (charOnTile != null)
            {
                var player = charOnTile.GetComponent<Player>();
                var enemy = charOnTile.GetComponent<Enemy>();
                if (player != null)
                {
                    switch (targets)
                    {
                        case AttackData.Target.allAllies:
                        case AttackData.Target.allCharacters:
                            targetsInAoe.Add(charOnTile);
                            targetsInAoe.Add(characterOnTile);
                            break;
                        case AttackData.Target.otherAllies:
                        case AttackData.Target.otherCharacters:
                            targetsInAoe.Add(charOnTile);
                            break;
                    }
                } else if(enemy != null)
                {
                    switch (targets)
                    {
                        case AttackData.Target.allEnemies: 
                        case AttackData.Target.allCharacters:
                        case AttackData.Target.otherCharacters:
                            targetsInAoe.Add(charOnTile);
                            break;
                    }
                }
            }
        }
        return targetsInAoe;
    }

    public List<List<GameObject>> FindShortestPathsTo(GameObject to)
    {
        List<List<GameObject>> routes = new();

        if (parent.name != to.name)
        {
            Queue<List<GameObject>> queue = new Queue<List<GameObject>>();
            queue.Enqueue(new List<GameObject> { parent });

            while (queue.Count > 0)
            {
                List<GameObject> currentPath = queue.Dequeue();
                GameObject currentTile = currentPath.Last();

                foreach (GameObject adjacent in currentTile.GetComponent<Tile>().connectedTiles)
                {
                    if (!currentPath.Contains(adjacent) && !adjacent.GetComponent<Tile>().IsObstructed())
                    {
                        List<GameObject> newPath = new List<GameObject>(currentPath){ adjacent };
                        queue.Enqueue(newPath);

                        if (adjacent == to)
                        {
                            routes.Add(newPath);
                        }
                    }
                }
            }
        }

        return routes;
    }

    public bool HasToken(Token.Type targetType)
    {
        foreach (GameObject t in tokens)
        {
            if (t.GetComponent<Token>().type == targetType)
            {
                return true;
            }
        }
        return false;
    }

    public void AddNewToken(Token.Type tokenType)
    {
        GameObject newObject = null;
        switch (tokenType)
        {
            case Token.Type.gas:
                newObject = Instantiate(gasPrefab, new Vector3(transform.position.x, gasPrefab.transform.position.y, transform.position.z), Quaternion.identity);
                break;
            case Token.Type.tree:
                newObject = Instantiate(treePrefab, new Vector3(transform.position.x, treePrefab.transform.position.y, transform.position.z), Quaternion.identity);
                break;
        }
        if (newObject != null)
        {
            newObject.transform.parent = parent.transform;
        }
    }

    public void AddNewTokens(List<Token.Type> tokens)
    {
        foreach (Token.Type token in tokens)
        {
            AddNewToken(token);
        }
    }

    public void RemoveToken(Token.Type targetType)
    {
        foreach (GameObject t in tokens)
        {
            if (t.GetComponent<Token>().type == targetType)
            {
                tokens.Remove(t);
                Destroy(t);
                break;
            }
        }
    }

    public bool IsType(Type targetType)
    {
        return type == targetType;
    }

    public void SetType(Type type)
    {
        this.type = type;
    }

    public List<GameObject> RemoveDuplicates(List<GameObject> list)
    {
        List<GameObject> newList = new() { };
        foreach(GameObject elem in list)
        {
            if (!newList.Contains(elem))
            {
                newList.Add(elem);
            }
        }
        return newList;
    }

    public bool IsObstructed()
    {
        foreach (GameObject token in tokens)
        {
            if (token.GetComponent<Tree>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public void AddOutlineShader()
    {
        if (outlineShader == null)
        {
            Debug.LogError("Outline Shader is not assigned!");
            return;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            var outlineMaterial = new Material(outlineShader);
            renderer.material = outlineMaterial;
        }
        else
        {
            Debug.LogError("Renderer component not found on the GameObject!");
        }
    }

    public void RemoveOutlineShader()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = originalMaterial;
        }
        else
        {
            Debug.LogError("Renderer component not found on the GameObject!");
        }
    }
}