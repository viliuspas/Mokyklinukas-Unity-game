using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //reikia, kad surastu UI is Unity
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Experimental.UIElements;

public class MokyklinukoValdymas : MonoBehaviour
{
    private Rigidbody2D rb; //Ridigbody2D traukia is Unity "Player" sekcijos
    private Animator anim; // Animator -,,-
    private Collider2D cl; //Collider2D -,,- ir  SerializeField paima HitBoxu informacija is Unity
    
    private enum Busena {idle, running, jumping, falling, hurt} //kiekviena animacijos busena isvardina skaiciais
    private Busena busen = Busena.idle;
    
    [SerializeField] private LayerMask ground;
    [SerializeField] private float greitis = 5f; //sukuria Unity -> player -> script -> greicio sekcija, kuria galima is Unity keisti.
    [SerializeField] private float suolis = 6.5f;
    [SerializeField] private Text NdText;
    [SerializeField] private Text Pokalbis;
    [SerializeField] private Text ParuostukasText;

    public int nd = 0;
    public int paruostukas = 0;
    public int perejimas = 0;
    
    private void Start() // kai zaidimas paleidziamas, programoje suranda isvardintus komponentus
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        
        //Gauna renkamųjų objektų pradinę informacija, paleidžiant scenai.
        nd = PlayerPrefs.GetInt("nd", 0);
        paruostukas = PlayerPrefs.GetInt("paruostukas", 0);

        //Kintamasis, padedantis išsaugoti durų perėjimus tarp scenų.
        perejimas = PlayerPrefs.GetInt("perejimas", 0);
       
        DuruPerejimas();
    }

    private void DuruPerejimas()
    {
        if (perejimas == 1)
        {
            transform.position = new Vector2(35.5f, -0.84f);
            GameObject.FindWithTag("MainCamera").transform.position = new Vector3(27f, 0.47f, -10f);

            //Grąžina durų perėjimo informaciją yra pradinę.
            perejimas = 0;
        }
    }

    private void Update() //Kiekviena kartai kai Unity atnaujina frames
    {
        if(busen != Busena.hurt)
        { 
            Judejimas();
        }
        JudejimoBusena();
        anim.SetInteger("busen", (int)busen); //paverciam "busen" i skaiciu, kuris nustatys player animacijos busena Unity programoje.
        NdText.text = nd.ToString();
        ParuostukasText.text = paruostukas.ToString();
        
    }

    private void Judejimas()
    {
        //Iš Unity -> player -> input - paima sekciją "Horizontal" ir ją valdo.
        float horiz = Input.GetAxis("Horizontal");

        if (horiz < 0) //Judėjimas į kairę.
        {
            rb.velocity = new Vector2(-greitis, rb.velocity.y); //Vector 2 tikrina x ir y.
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));

        }

        else if (horiz > 0) //Judėjimas i desine
        {
            rb.velocity = new Vector2(greitis, rb.velocity.y);
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
        }

        //Jeigu paspaudžiamas Space ir objektų ribos liecia vienos kitą (šiuo atveju player ir ground).
        if (Input.GetButtonDown("Jump") && cl.IsTouchingLayers(ground)) 
        {
            rb.velocity = new Vector2(rb.velocity.x, suolis);
            busen = Busena.jumping;
        }
    }

    private void JudejimoBusena()
    {
        if(busen == Busena.jumping)
        {
            if(rb.velocity.y < Mathf.Epsilon) //Jeigu player leidziasi zemyn
            {
                busen = Busena.falling;
            }
        }
        
        else if(busen == Busena.falling)
        {
            if (cl.IsTouchingLayers(ground))
            {
                busen = Busena.idle;
            }
        }

        else if(busen == Busena.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < 1f) //jeigu greitis yra beveik nulis (Mathf.Abs uzdeda moduli, kad butu galima nustatyti leciausia judejima ir i kaire ir i desine)
            {
                busen = Busena.idle;
            }
        }
        
        else if(Mathf.Abs(rb.velocity.x) > Mathf.Epsilon) //Mathf - funkciju kolekcija; Abs - absoliutus skaicius; Epsilon - maziausias galimas skaicius. Jeigu vyksta x judejimas, player bega.
        {
            busen = Busena.running;
        }
       
        else
        {
            busen = Busena.idle;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //objektai / tranzicijos
    {
        if(collision.tag == "Renkamas") //jeigu zaidejas priliecia daikta su tag - "Renkamas" - tai ji sunaikins.
        {
            Destroy(collision.gameObject);
            nd++; //skaiciuoja lentelei skaiciu rodyti
            NdText.text = nd.ToString(); //is NdText paima sekcija "text" = nd is int pavercia i string
        }

        else if (collision.tag == "Paruostukas")
        {
            Destroy(collision.gameObject);
            paruostukas++;
            ParuostukasText.text = paruostukas.ToString();
        }

        else if (collision.tag == "Apacia")
        {
            nd = 0;
            SceneManager.LoadScene("MirtiesEkranas");
        }

        else if (collision.tag == "Galas")
        {
            if(nd >= 9)
            {
                SceneManager.LoadScene("PirmaPabaiga");
            }
        }
        else if(collision.tag == "Galas2")
        {
            GameObject.FindWithTag("MainCamera").transform.position = new Vector3(27f, 0.47f, -10f); //AntrasLygis1 antra kameros pozicija
            transform.position = new Vector2(14.66f, -0.5304108f);
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
        }
        else if(collision.tag == "Galas3")
        {
            GameObject.FindWithTag("MainCamera").transform.position = new Vector3(0f, 0.47f, -10f); //AntrasLygis1 pirma kameros pozicija
            transform.position = new Vector2(11.98f, -0.5304108f);
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
        }

        //Jeigu eina iš rūsio.
        else if(collision.tag == "Galas4")
        {
            perejimas = 1;
            //Atsiduria prie blatų durų naujoje scenoje.
            SceneManager.LoadScene("AntrasLygis1");
            
        }
    }
    
    //Kai objektas susiduria su kitu objektu.
    private void OnCollisionEnter2D(Collision2D susidurimas) 
    {
        //Jeigu susiduria su priešu.
        if (susidurimas.gameObject.tag == "Priesas")
        {
            //Jeigu Mokyklinukas krenta.
            if (busen == Busena.falling)
            {
                //Mokyklinukas atšoks.
                rb.velocity = new Vector2(rb.velocity.x, suolis);
                busen = Busena.jumping;
            }
            else
            {
                busen = Busena.hurt;
                
                //jeigu priešas yra desinėje nuo žaidėjo.
                if (transform.position.x < susidurimas.gameObject.transform.position.x) 
                {
                    //žaidėjas pajudės į kairę.
                    rb.velocity = new Vector2(-5f, rb.velocity.y); 
                }
                else
                {   //žaidėjas pajudės į dišinę.
                    rb.velocity = new Vector2(5f, rb.velocity.y); 
                }
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("AntrasLygis2"))
        {
            if (collision.tag == "Pokalbis")
            {
                if (nd >= 9)
                {
                    Pokalbis.text = "[E] kalbėti";
                    if (Input.GetKey(KeyCode.E))
                    {
                        GameObject.FindWithTag("Draugas").gameObject.GetComponent<Collider2D>().isTrigger = true;
                        GameObject.FindWithTag("Draugas").transform.localScale = new Vector2(1.5f, 1.5f);
                        Pokalbis.text = " ";
                        GameObject.FindWithTag("Pokalbis").gameObject.GetComponent<Collider2D>().enabled = false;
                        nd = nd - 9;
                    }
                }
                else Pokalbis.text = "Trūksta namų darbų. Grįžk kai turėsi 9.";


            }
            else
            {
                Pokalbis.text = " ";
            }
        }
    }

    //Prieš sunaikinant sceną, objektai išsaugomi
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("nd", nd);
        PlayerPrefs.SetInt("paruostukas", paruostukas);
        PlayerPrefs.SetInt("perejimas", perejimas);
    }
}
