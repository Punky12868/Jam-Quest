using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] float reduceLightDistance;
    Vector2 respawnPoint;

    [SerializeField] private KeyCode kys;

    private void Awake()
    {
        ResetSpawnPoint();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnDeath();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(kys))
        {
            OnDeath();
        }

        bool reduce = transform.position.x > respawnPoint.x - reduceLightDistance && transform.position.x < respawnPoint.x + reduceLightDistance;
        if (!reduce)
        {
            ReduceLight.SetReduce(true);
        }
    }

    public void OnDeath()
    {
        //transform.position = respawnPoint;
        ReduceLight.SetReduce(false);
        GetComponent<Damage>().OnDamage();
        FindObjectOfType<RestartController>().LoadProgress();
        AudioManager.instance.PlaySFX(2);

        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in enemies)
        {
            enemy.playerInSight = false;
        }
    }

    public void SetRespawnPoint(Vector2 point)
    {
        respawnPoint = point;
    }
    public Vector2 GetSpawnPoint()
    {
        return respawnPoint;
    }
    public void ResetSpawnPoint()
    {
        respawnPoint = transform.position;
    }
}
