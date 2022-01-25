using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    EnemyHealth enemyHealth;
    [SerializeField] ParticleSystem deathFX;

    private void Awake() {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            
            if (playerHealth.GetInvincibility()){ // if player is invincible, only enemy respawns
                if(deathFX != null)
                    Instantiate(deathFX,transform.position,Quaternion.identity); 
                enemyHealth.EnemyRespawn();
            }
            else{
                playerHealth.PlayerRespawn();
                transform.parent.gameObject.BroadcastMessage("EnemyRespawn");       // call respawn for all enemies
            }
        }

    }
}
