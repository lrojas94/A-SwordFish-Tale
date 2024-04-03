using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleUnit))]
public class Player : MonoBehaviour
{
    [HideInInspector]
    public BattleUnit battleUnit;
    public float movementSpeed = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        battleUnit = GetComponent<BattleUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = -Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        if (xAxis != 0)
        {
            transform.position += (Vector3.left * movementSpeed * Time.deltaTime * xAxis);
        }   

        if (yAxis != 0)
        {
            transform.position += (Vector3.up * movementSpeed * Time.deltaTime * yAxis);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.collider.tag == "Enemy")
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            TurnBattleManager.Instance.StartBattle(enemy);
        }
    }
}
