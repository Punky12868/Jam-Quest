using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector2 playerPosition;

    [SerializeField] private KeyCode abilityKey;
    private void Awake()
    {
        playerPosition = player.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(abilityKey))
        {
            player.position = playerPosition;
            ReduceLight.SetReduce(false);
        }

        if (player.position.x != playerPosition.x)
        {
            ReduceLight.SetReduce(true);
        }
    }
}
