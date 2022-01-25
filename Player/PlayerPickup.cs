using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    ItemPickup itemPickup; 
    PlayerHealth playerHealth;

    AudioSource audioSource;
    [SerializeField] AudioClip eatingSFX, invincibleSFX;
    [SerializeField] ParticleSystem invincibleFX;
    ParticleSystem instantiatedParticle;

    MenuManager menuManager;

    private void Awake() {
        playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        menuManager = FindObjectOfType<MenuManager>();
    }
    
    private void Update() {
        if (instantiatedParticle !=null && !playerHealth.GetInvincibility())
            Destroy(instantiatedParticle.gameObject);  // remove particle at the end of invincibility
    }

    void OnTriggerEnter(Collider item)
    {
        itemPickup = item.GetComponent<ItemPickup>();

        if (itemPickup){
            item.gameObject.SetActive(false);
            ScoreManager.Instance.AddScore(itemPickup.getScore());

            if (GameObject.FindWithTag("Pickup") == null)
            {
                menuManager.LevelComplete();
            }

            if (itemPickup.getItemType() == ItemPickup.ItemType.normal && eatingSFX!=null)
                    audioSource.PlayOneShot(eatingSFX);

            else if (itemPickup.getItemType() == ItemPickup.ItemType.invincibility){
                playerHealth.SetInvincibility(true);
                if (invincibleFX != null){
                    instantiatedParticle = Instantiate(invincibleFX, transform.position, Quaternion.Euler(-90,0,0));  
                    instantiatedParticle.transform.parent = transform;                      //created under player
                }
                if (invincibleSFX != null)
                    audioSource.PlayOneShot(invincibleSFX);
            }
        }
            
    }
}
