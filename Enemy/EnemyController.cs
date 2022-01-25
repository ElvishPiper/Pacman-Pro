using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float wanderRadius = 5f;

    private Transform player;
    private PlayerHealth playerHealth;
    
    private NavMeshAgent enemy;
    private EnemyHealth health;

    private float wanderTimer, timer;

    private enum State {WANDER,CHASE,FLEE}
    private State currentState;

    [SerializeField] Material fleeMaterial;
    Material originalMaterial;
    Renderer enemyRenderer; 

    
    private enum Personality{Wanderer, Chaser, Patrol} //Wanderer: always wanders, Chaser: always chases, Patrol: wander first, chase if in Line of Sight
    [SerializeField] private Personality personality;

    private void Awake() {
        enemy = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyRenderer = GetComponent<Renderer>();
        originalMaterial = enemyRenderer.material;
    }

    private void Start() {
        ResetState();
    }

    void Update()
    {
        if (playerHealth.GetInvincibility()){
            currentState = State.FLEE;
        }
        if (currentState == State.FLEE && !playerHealth.GetInvincibility()){
            ResetState();
        }

        if (health.IsDead())
            return;
        
        if (personality == Personality.Patrol && currentState != State.FLEE)
            CheckLineOfSight();

        timer += Time.deltaTime;
    
        //FSM states
        if (currentState == State.WANDER)
        {
            if (timer >= wanderTimer){
                enemy.SetDestination(RandomNavSphere(transform.position, wanderRadius));
                ResetTimers();
            }
        }
        else if (currentState == State.CHASE){
            ChasePlayer();
        }
        else if (currentState == State.FLEE){
            enemy.ResetPath();
            Flee();
        }
    }

    private void ResetState(){
        ResetTimers();
        SetPersonalityAction();
        ChangeColors();
    }

    private void ResetTimers()
    {
        timer = 0f;
        wanderTimer = Random.Range(1f, 3f); // wander around every random time between 1-5s
    }

    private static Vector3 RandomNavSphere(Vector3 position, float wanderRadius)
    {
        Vector3 randDirection = Random.insideUnitSphere * wanderRadius + position;
        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, wanderRadius, NavMesh.AllAreas);

        return navHit.position;
    }

    private void ChasePlayer()=>enemy.SetDestination(player.position);
    
    private void Flee(){
        Vector3 fleeDirection = transform.position + (transform.position - player.transform.position);
        NavMesh.SamplePosition(fleeDirection, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas);
        enemy.SetDestination(navHit.position);
        ChangeColors();
    }

    private void ChangeColors()
    {
        if (fleeMaterial is null) return;

        if (currentState != State.FLEE)
            enemyRenderer.material = originalMaterial;
        else
            enemyRenderer.material = fleeMaterial;
    }

    private void SetPersonalityAction(){
        switch(personality){
            case Personality.Wanderer:
                currentState = State.WANDER;
                break;
            case Personality.Chaser:
                currentState = State.CHASE;
                break;
            case Personality.Patrol:
                currentState = State.WANDER;
                break;
        }
    }

    private void CheckLineOfSight(){
        if (!enemy.Raycast(player.position, out NavMeshHit hit))
            currentState = State.CHASE; 
        else
            currentState = State.WANDER;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
