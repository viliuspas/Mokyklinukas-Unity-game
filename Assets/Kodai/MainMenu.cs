using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Paleisti()
    {
        SceneManager.LoadScene("PirmaIstorija");
    }

    public void Toliau()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void Iseiti()
    {
        Application.Quit();
    }
}
