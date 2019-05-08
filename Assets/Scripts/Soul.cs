using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Sprite[] sprites;

    private Player player;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        int randIndex = Random.Range(0, sprites.Length);
        spr.sprite = sprites[randIndex];
        player = FindObjectOfType<Player>();
        StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().CollectSoul();
            Destroy(gameObject);
        }
    }
}
