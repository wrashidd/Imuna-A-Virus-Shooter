using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
This class is responsible for Ending and Restarting the game
*/
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;

    // Update is called once per frame
    // Restarts the level on user input when game is over
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
