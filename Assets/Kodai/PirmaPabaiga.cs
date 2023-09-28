using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PirmaPabaiga : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AntrasLygis()
    {
        SceneManager.LoadScene("AntrasLygis1");
    }
}
