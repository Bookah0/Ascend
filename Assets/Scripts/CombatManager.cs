using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

public enum CombatState { Start, PlayerTurn, EnemyTurn, Won, Lost }

public class CombatManager : MonoBehaviour
{
    public Battlefield battlefield;
    private List<GameObject> players = new();
    private List<GameObject> npcs = new();
    private List<GameObject> enemies = new();
    public GameObject currentTurnCharacter;
    public List<GameObject> tiles = new();
    public Character currentCharacterScript;
    public Movement currentMovementScript;
    public Stats currentStatsScript;
    public CombatState state;
    public Queue<GameObject> turnOrder = new();
    private LayerMask ignoreLayer;
    public bool usingAbility = false;
    public Ability curAbility = null;

    public Dictionary<KeyCode, int> abilityControls = new()
    {
        { KeyCode.Alpha1, 1 },
        { KeyCode.Alpha2, 2 }
    };


    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(playerObjects.Length);
        players.AddRange(playerObjects);
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.AddRange(enemyObjects);
        ignoreLayer = LayerMask.GetMask("Tile Token Object");
        StartCombat();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == CombatState.PlayerTurn)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, ~ignoreLayer))
            {
                HandleTileHover();
            }

            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            if (scrollDelta != 0f)
            {
                HandleScrollWheel(scrollDelta);
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleLeftMouseClick();
            }

            if (Input.GetMouseButtonDown(1))
            {
                usingAbility = false;
                curAbility = null;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextTurn();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                usingAbility = true;
                curAbility = currentTurnCharacter.GetComponent<Player>().abilitySlots[0];
            }
        }
        else
        {
            currentMovementScript.MoveToClosestPlayer(currentCharacterScript, players);
            NextTurn();
        }
    }

    private void HandleTileHover()
    { 
        GameObject hoveredObject = hit.collider.gameObject;
        if (hoveredObject.GetComponent<Tile>() != null && hoveredObject != currentTurnCharacter.GetComponent<Player>().position)
        {
            currentMovementScript.HoverOverTile(currentCharacterScript, hoveredObject, tiles);
        }
        else
        {
            currentMovementScript.ResetTileHovering(tiles);
        }
    }

    private void HandleLeftMouseClick()
    {
        if (!usingAbility)
        {
            currentMovementScript.MoveToSelectedTile(currentCharacterScript);
        }
        else
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                Enemy enemyComponent = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    Debug.Log(hit.collider.gameObject);
                    curAbility.Activate(new List<GameObject> { hit.collider.gameObject });
                }
                else
                {
                    Debug.Log("Enemy script is null");
                }
            }
        }
    }

    private void HandleScrollWheel(float scrollDelta)
    {  
        if (currentMovementScript.routes != null)
        {
            if (scrollDelta > 0f)
            {
                currentMovementScript.NextRoute(tiles);
            }
            else
            {
                currentMovementScript.PreviousRoute(tiles);
            }
        }
    }

    public void ReadyAbility()
    {
        usingAbility = true;
    }

    private void StartCombat()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().MoveToSpawnPosition();
        }
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().MoveToSpawnPosition();
        }
        
        InitTurnOrder();
        NextTurn();
    }

    private void NextTurn()
    {
        if(currentTurnCharacter != null)
        {
            turnOrder.Enqueue(currentTurnCharacter);
        }

        currentTurnCharacter = turnOrder.Dequeue();
        if (currentTurnCharacter.GetComponent<Player>() != null)
        {
            state = CombatState.PlayerTurn;
            currentCharacterScript = currentTurnCharacter.GetComponent<Player>();
        }
        else
        {
            state = CombatState.EnemyTurn;
            currentCharacterScript = currentTurnCharacter.GetComponent<Enemy>();
        }
        currentStatsScript = currentTurnCharacter.GetComponent<Stats>();
        currentStatsScript.NewTurn();
        currentMovementScript = currentTurnCharacter.GetComponent<Movement>();
    }

    private void InitTurnOrder()
    {
        Debug.Log(players.Count);
        Debug.Log(enemies.Count);

        var initiativeDict = new Dictionary<GameObject, int>();
        foreach (GameObject enemy in enemies)
        {
            initiativeDict.Add(enemy, enemy.GetComponent<Stats>().RollInitiative(enemy));
        }
        foreach (GameObject player in players)
        {
            initiativeDict.Add(player, player.GetComponent<Stats>().RollInitiative(player));
        }

        var sortedInitiativeDict = initiativeDict.OrderByDescending(kv => kv.Value);

        foreach (var kvp in sortedInitiativeDict)
        {
            turnOrder.Enqueue(kvp.Key);
        }
    }
}
