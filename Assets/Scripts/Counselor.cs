using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counselor : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 0;
    [SerializeField]
    private float maxSpeed = 0;
    [SerializeField]
    private GameObject soulPrefab;
    [SerializeField]
    private Splat splat;
    [SerializeField]
    private int numberOfSplats;

    private float speed;

    private Vector3 direction;

    private Player player;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        player = FindObjectOfType<Player>();
        rb2d = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(transform.position + direction.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        Instantiate(soulPrefab, transform.position, Quaternion.identity);
        Vector3 lastDir = direction.normalized;
        for (int i = 0; i < numberOfSplats; i++)
        {
            Vector3 sprayDir = SprayDirection(lastDir);
            Splat splatObj = Instantiate(splat, transform.position + sprayDir, Quaternion.identity);
            splatObj.Initialize();
        }
    }

    Vector3 SprayDirection(Vector2 v)
    {
        Debug.Log("Dir: " + v);
        float randomDirMult = Random.Range(2, 8);
        float randomDirAdd = Random.Range(0, 1);
        Vector2 sprayDirection = v;

        // if positive or zero
        if (v.x > 0f)
        {
            Debug.Log("X is positive");
            // make negative
            sprayDirection.x = -v.x;
        }
        else
        {
            Debug.Log("X is negative");
            sprayDirection.x = Mathf.Abs(v.x);
        }

        if (v.y > 0f)
        {
            Debug.Log("Y is positive");
            sprayDirection.y = -v.y;
        }
        else
        {
            Debug.Log("Y is negative");
            sprayDirection.y = Mathf.Abs(v.y);
        }

        Debug.Log("SprayDir: " + sprayDirection);

        if (Mathf.Abs(sprayDirection.x) > Mathf.Abs(sprayDirection.y))
        {
            Debug.Log("X is greater");
            sprayDirection.x *= randomDirMult;
            sprayDirection.y += randomDirAdd;
        }
        else
        {
            Debug.Log("Y is greater");
            sprayDirection.y *= randomDirMult;
            sprayDirection.x += randomDirAdd;
        }

        Debug.Log("SprayDirMod: " + sprayDirection);

        return new Vector3(sprayDirection.x, sprayDirection.y, 0);
    }
}
