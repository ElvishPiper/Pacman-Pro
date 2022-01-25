using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    private bool dead = false; 
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float spawnFreeze = 2f;
    
    private NavMeshAgent enemy;

    private void Awake() {
        enemy = GetComponent<NavMeshAgent>();
    }
    
    public void EnemyRespawn(){
        dead = true;
        enemy.Warp(spawnPoint.transform.position);
        enemy.ResetPath();
        StartCoroutine(WaitAtSpawn());
    }

    IEnumerator WaitAtSpawn(){
        yield return new WaitForSeconds(spawnFreeze);
        dead = false;
    }

    public bool IsDead()=>dead;


}
