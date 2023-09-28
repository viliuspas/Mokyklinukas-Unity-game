using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Durys : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Text text;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Kol yra ant durų
    private void OnTriggerStay2D(Collider2D collision)
    {
        anim.SetBool("atviros", false);
        if (collision.gameObject.tag == "Player")
        {
            text.text = "[R] Įeiti į kabinetą";
            if(Input.GetKey(KeyCode.R))
            {
                anim.SetBool("atviros", true);
            }  
        }
    }
    
    //Kai palieka duris
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("atviros", false);
        text.text = " ";
    }

    //void f-ja reikalinga, kad butų galima nustatyti Animation event.
    private void Atsidaro()
    {
        SceneManager.LoadScene("AntrasLygis1");
    }

    private void Rusys()
    {
        SceneManager.LoadScene("AntrasLygis2");
    }

}
