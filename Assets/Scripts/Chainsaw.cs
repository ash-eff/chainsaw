using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    [SerializeField]
    private GameObject blood;
    [SerializeField]
    private int numberOfSplats;
    [SerializeField]
    private Splat splat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Counselor")
        {
            Vector3 center = transform.position;
            GameObject bloodObj = Instantiate(blood, collision.transform.position, Quaternion.identity);
            Destroy(bloodObj, .5f);
            for (int i = 0; i < numberOfSplats; i++)
            {
                float rad = Random.Range(1, 4);
                Vector3 pos = RandomCircle(center, rad);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                Splat splatObj = Instantiate(splat, pos, rot);
                splatObj.Initialize();
            }

            Destroy(collision.gameObject);
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
