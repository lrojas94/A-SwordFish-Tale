using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBattleManager : MonoBehaviour
{
    public static TurnBattleManager Instance;

    public List<Transform> playerPositionSlots;
    public List<Transform> enemyPositionSlots;
    public Vector3 playerStartPosition;
    public Vector3 enemyStartPosition;

    public GameObject mainPrefab;
    
    private List<GameObject> playerObjects;
    private List<GameObject> enemyObjects;
    private Player player;
    enum BattleState
    {
        SETUP = 0,
        PLAYER_ACTION_SELECTION = 1,
        BATTLE_SEQUENCE = 2,
        BATTLE_WON = 3,
        BATTLE_LIST = 4
    }

    private BattleState battleState;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void StartBattle(Enemy enemy)
    {
        battleState = BattleState.SETUP;
        StartCoroutine(BattleSetup(enemy));
    }

    IEnumerator BattleSetup(Enemy enemy)
    {
        // Destroy all prev. objects:
        this.playerStartPosition = player.transform.position;
        this.enemyStartPosition = enemy.transform.position;

        playerObjects = new List<GameObject> {  player.gameObject };
        enemyObjects = new List<GameObject> { enemy.gameObject };

        Debug.Log($"{enemyPositionSlots.Count}, {enemyPositionSlots[0].position}");

        // Instantiate enemy party:
        enemy.battleUnit.Setup(enemyPositionSlots[0].position);

        int slot = 1;
        enemy.party.ForEach(f =>
        {
            GameObject enemyObject = GameObject.Instantiate(mainPrefab);
            enemyObject.transform.position = enemyStartPosition;
            BattleUnit unit = enemy.GetComponent<BattleUnit>();
            Debug.Log(f.name);
            unit.Setup(f, enemyPositionSlots[slot].position);
            slot++;
        });

        // Set player up:
        player.battleUnit.Setup(playerPositionSlots[0].position); // TODO: Adjust entire party

        while (true)
        {
            GameObject playerObjectsSetup = playerObjects.Find(x =>
            {
                BattleUnit unit = x.GetComponent<BattleUnit>();
                if (!unit.IsSetupComplete())
                {
                    return true;
                }

                return false;
            });

            GameObject enemyObjectsSetup = enemyObjects.Find(x =>
            {
                BattleUnit unit = x.GetComponent<BattleUnit>();
                if (!unit.IsSetupComplete())
                {
                    return true;
                }

                return false;
            });

            if (playerObjectsSetup == null && enemyObjects == null) {
                battleState = BattleState.PLAYER_ACTION_SELECTION;
                break;
            }
            
            yield return null;
        }
    }
}
