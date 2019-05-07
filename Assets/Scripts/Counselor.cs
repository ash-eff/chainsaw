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
    }
}
