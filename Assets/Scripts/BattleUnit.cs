using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    // Start is called before the first frame update
    public Fish fish;
    public float currentHealth;

    enum BattleUnitState
    {
        SETUP = 0,
        IDLE = 1,
        ATTACK = 2,
        BACK_TO_POSITION = 3,
    }

    private BattleUnitState state;
    private Vector3 targetPosition;
    private float speed = 10f;

    public void Awake()
    {
        if (fish != null)
        {
            Setup(fish, transform.position);
        }
    }

    public void Setup(Fish fish, Vector3 targetPosition)
    {
        Debug.Log(fish);
        this.fish = fish;
        currentHealth = fish.health;
        
        // TODO: Update sprites here.
        this.targetPosition = targetPosition;
        state = BattleUnitState.SETUP;
    }

    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        state = BattleUnitState.SETUP;
    }

    public void Update() {
        if (state == BattleUnitState.SETUP)
        {
            float minDistance = 0f;

            if (Vector3.Distance(targetPosition, transform.position) > minDistance)
            {
                Debug.Log(targetPosition);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            } else
            {
                state = BattleUnitState.IDLE;
            }
        }
    }

    public bool IsSetupComplete()
    {
        return state != BattleUnitState.SETUP;
    }

}
