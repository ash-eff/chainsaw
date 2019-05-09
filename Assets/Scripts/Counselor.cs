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
    [SerializeField]
    private GameObject leftArm;
    [SerializeField]
    private GameObject rightArm;

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

        Vector3 targetDirRight = player.transform.position - rightArm.transform.position;
        Vector3 targetDirLeft = player.transform.position - leftArm.transform.position;

        float angleRight = Mathf.Atan2(targetDirRight.y, targetDirRight.x) * Mathf.Rad2Deg;
        float angleLeft = Mathf.Atan2(targetDirLeft.y, targetDirLeft.x) * Mathf.Rad2Deg;

        Quaternion rotRight = Quaternion.AngleAxis(angleRight, Vector3.forward);
        Quaternion rotLeft = Quaternion.AngleAxis(angleLeft, Vector3.forward);

        rightArm.transform.rotation = Quaternion.Slerp(rightArm.transform.rotation, rotRight, Time.deltaTime * speed);
        leftArm.transform.rotation = Quaternion.Slerp(leftArm.transform.rotation, rotLeft, Time.deltaTime * speed);
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
        float randomDirMult = Random.Range(2, 8);
        float randomDirAdd = Random.Range(0, 1);
        Vector2 sprayDirection = v;

        // if positive or zero
        if (v.x > 0f)
        {
            // make negative
            sprayDirection.x = -v.x;
        }
        else
        {
            sprayDirection.x = Mathf.Abs(v.x);
        }

        if (v.y > 0f)
        {
            sprayDirection.y = -v.y;
        }
        else
        {
            sprayDirection.y = Mathf.Abs(v.y);
        }

        if (Mathf.Abs(sprayDirection.x) > Mathf.Abs(sprayDirection.y))
        {
            sprayDirection.x *= randomDirMult;
            sprayDirection.y += randomDirAdd;
        }
        else
        {
            sprayDirection.y *= randomDirMult;
            sprayDirection.x += randomDirAdd;
        }
        return new Vector3(sprayDirection.x, sprayDirection.y, 0);
    }
}
