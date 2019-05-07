using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    [SerializeField]
    private float minSize = 0.8f;
    [SerializeField]
    private float maxSize = 1.5f;
    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        float randomTime = Random.Range(10, 20);
        Destroy(gameObject, randomTime);
    }

    public void Initialize()
    {
        SetSprite();
        SetSize();
        SetRotation();
    }

    private void SetSprite()
    {
        int randomIndex = Random.Range(0, sprites.Length);
        spr.sprite = sprites[randomIndex];
    }

    private void SetSize()
    {
        float sizeMod = Random.Range(minSize, maxSize);
        transform.localScale *= sizeMod;
    }

    private void SetRotation()
    {
        float randomRot = Random.Range(-360f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
    }
}
