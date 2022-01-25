using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;

    private static int playerLives = 3;
    private int initialPlayerLives = 3;
    [SerializeField] private float spawnFreeze = 2f;
    [SerializeField] private float invincibilityTimer = 3f;

    [SerializeField] ParticleSystem deathFX;
    [SerializeField] AudioClip deathSFX;
    AudioSource audioSource;

    private bool invincible = false; 
    private bool dead = false; 
    private float timer = 0f;

    MenuManager menuManager;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        menuManager = FindObjectOfType<MenuManager>();
    }

    void Start()
    {
        ScoreManager.Instance.updateLivesText(playerLives);
    }

    private void Update() {
        if(invincible){
            timer+=Time.deltaTime;
            menuManager.GetComponentInChildren<Slider>(true).gameObject.SetActive(true);
            float displayValue = Mathf.Lerp(1f, 0f, timer/invincibilityTimer);
            menuManager.GetComponentInChildren<Slider>().value = displayValue;
            if(timer>= invincibilityTimer){
                SetInvincibility(false); // turn invincibility off after set amount 
                timer = 0f;
                menuManager.GetComponentInChildren<Slider>().gameObject.SetActive(false);
            }
        }
    }

    
    public void PlayerRespawn(){
        if(deathFX != null)
            Instantiate(deathFX,transform.position,Quaternion.Euler(0,0,90));  //facing up
        if(deathSFX!=null)
            audioSource.PlayOneShot(deathSFX);

        dead=true;
        playerLives -= 1;
        ScoreManager.Instance.updateLivesText(playerLives);

        if (playerLives <=0){
            //game over screen
            menuManager.GameOver();
        }

        transform.position = spawnPoint.transform.position;  //respawn
        StartCoroutine(WaitAtSpawn());
        
    }

    IEnumerator WaitAtSpawn(){
        yield return new WaitForSeconds(spawnFreeze);
        dead = false;
    }
    

    public bool GetInvincibility() => invincible;
    public void SetInvincibility(bool isInvincible)=>invincible=isInvincible;
    
    public bool IsDead()=>dead;

    public float GetInvincibilityTimer()=>invincibilityTimer;

    public void resetLives()=> playerLives = initialPlayerLives;

}
