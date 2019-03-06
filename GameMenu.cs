using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);

    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
