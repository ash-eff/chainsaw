using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem blood;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Counselor")
        {
            blood.Play();
            Destroy(collision.gameObject);
        }
    }
}
