using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get; private set;}

    private static int score;

    private Text scoreText, livesText;

    private void Awake() {
        if(!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        scoreText = transform.Find("Score").GetComponent<Text>();
        livesText = transform.Find("Lives").GetComponent<Text>();
    }

    public void AddScore(int amount){
        score += amount;
        scoreText.text = $"Score: {score}";
    }

    public int getScore()=>score;

    public void updateLivesText(int lives){
        livesText.text = $"Lives: {lives}";
    }

    public void resetScore()=>score=0;
  
}
