using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldGenerator : MonoBehaviour
{
    //[SerializeField] public TileData[] tileData;
    //[SerializeField] public EnemiesToSpawn[] enemiesToSpawn;
    //[SerializeField] public Dictionary<GameObject, (Tile.Type, List<Token.TokenType>)> tileData;
    public Dictionary<GameObject, GameObject> enemiesToSpawn;


    void Start()
    {
        //UpdateTiles();
        //SpawnEnemies();
    }
    /*
    private void UpdateTiles()
    {
        foreach (var (tile, (type, tokens)) in tileData)
        {
            tile.GetComponent<Tile>().AddNewTokens(tokens);
            tile.GetComponent<Tile>().SetType(type);
        }
    }

    private void SpawnEnemies()
    {
        foreach (var (tile, enemy) in enemiesToSpawn)
        {
            var spawnPos = new Vector3(tile.transform.position.x, enemy.transform.position.y, tile.transform.position.z);
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }

    [System.Serializable]
    public struct TileData
    {
        public GameObject tile;
        public List<Token.TokenType> tokens;
        public Tile.Type type;

        public TileData(GameObject tile, List<Token.TokenType> tokens, Tile.Type type)
        {
            this.type = type;
            this.value = value;
        }

    }
    public DmgResistances[] resistances;
    public DmgResistances[] grazeResistances;
    public DmgResistances[] critResistances;
    public int GetDamageResistance(Damage.Type type, DmgResistances[] resArr)
    {
        foreach (DmgResistances res in resArr)
        {
            if (res.type == type)
            {
                return res.value;
            }
        }
        return 0;
    }

    [System.Serializable]
    public struct StatIncreases
    {
        public Stats.StatType type;
        public int value;

        public StatIncreases(Stats.StatType type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }

    public StatIncreases[] statIncreases;

    public int GetStatIncrease(Stats.StatType type, StatIncreases[] resArr)
    {
        foreach (StatIncreases res in resArr)
        {
            if (res.type == type)
            {
                return res.value;
            }
        }
        return 0;
    }
    */
}
