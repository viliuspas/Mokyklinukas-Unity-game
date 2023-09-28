using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiurkesValdymas : MonoBehaviour
{
    [SerializeField] private float kaire;
    [SerializeField] private float desine;
    [SerializeField] private float greitis;
    private Rigidbody2D rb;
    private bool ZiuriKairen = true; //bool - true arba false.

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (ZiuriKairen == true)
        {
            if (transform.position.x > kaire)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                rb.velocity = new Vector2(-greitis, rb.velocity.y);
            }
            else
            {
                ZiuriKairen = false;
            }
        }
        else
        {
            if (transform.position.x < desine)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                rb.velocity = new Vector2(greitis, rb.velocity.y);
            }
            else
            {
                ZiuriKairen = true;
            }
        }
    }
}
