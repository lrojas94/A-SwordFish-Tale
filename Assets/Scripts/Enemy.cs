using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleUnit))]
public class Enemy : MonoBehaviour
{
    public List<Fish> party;
    [HideInInspector]
    public BattleUnit battleUnit;

    private void Awake()
    {
        battleUnit = GetComponent<BattleUnit>();
    }
}
