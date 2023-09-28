using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamerosValdymas : MonoBehaviour
{
    public Transform player;
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); //Vector 3 tikrina x y z. Transform.position keicia prasomo dalyko pozicija. player.position.y ir x ima player pozicija is programos.
    }
}
