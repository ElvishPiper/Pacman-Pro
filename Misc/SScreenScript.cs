using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(Time5());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Time5()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menus");
    }
    
}
