using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        // if R is pressed, restart the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            // restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
