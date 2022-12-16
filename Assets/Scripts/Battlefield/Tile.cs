using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Token
    {
        frozen,
        tree,
        burning,
        wet,
        gas,
        mist,
        bush,
        rubble
    }

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

    public enum Blockage
    {
        none,
        stone,
        ice,
        wood
    }

    public List<Token> tokens = new List<Token> { };
    public Blockage blockage = Blockage.none;
    public Type type;

    public bool HasToken(Tile.Token token)
    {
        return (tokens.Contains(token));
    }

    public void FellTree()
    {
        tokens.Remove(Token.tree);
    }

    public void DestroyBlockage(Blockage blockage)
    {
        this.blockage = Blockage.none;
        switch (blockage)
        {
            case Blockage.ice:
                break;
            case Blockage.stone:
                tokens.Add(Token.rubble);
                break;
            case Blockage.wood:
                break;
        }
    }

    public void DestroyConnectedBlockage() { }

    public List<GameObject> CharactersInAoe(float area) {
        return new List<GameObject> { };
    }

}
