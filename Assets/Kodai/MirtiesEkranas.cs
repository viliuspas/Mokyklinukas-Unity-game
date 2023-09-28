using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirtiesEkranas : MonoBehaviour
{
    public void Again()
    {
        SceneManager.LoadScene("PirmasLygis");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
