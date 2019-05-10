using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float startingHealth;
    [SerializeField]
    private float dmgTaken;
    [SerializeField]
    private GameObject chainSaw;
    [SerializeField]
    private Image healthAmount;
    [SerializeField]
    private TextMeshProUGUI soulsAmountText;
    [SerializeField]
    private Vector2 maxClamp;
    [SerializeField]
    private Vector2 minClamp;

    public float health;
    public int souls;

    [SerializeField]
    private Animator anim;
    private Rigidbody2D rb2d;
    private Vector3 velocity;

    private void Awake()
    {
        health = startingHealth;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.GameOver)
        {
            return;
        }

        if(health <= 0)
        {
            GameController.instance.GameOver = true;
        }

        healthAmount.fillAmount = health / startingHealth;

        soulsAmountText.text = souls.ToString("0000");

        Move();
        WeaponRotation();
    }

    private void FixedUpdate()
    {
        Vector3 movePos = transform.position + velocity.normalized * speed * Time.deltaTime;

        movePos.x = Mathf.Clamp(movePos.x, minClamp.x, maxClamp.x);
        movePos.y = Mathf.Clamp(movePos.y, minClamp.y, maxClamp.y);
        
        rb2d.MovePosition(movePos);
    }

    void Move()
    {
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        if (Mathf.Abs(velocity.x) > 0 || Mathf.Abs(velocity.y)> 0)
        {
            anim.SetTrigger("Walking");
        }
        else
        {
            anim.ResetTrigger("Walking");
        }
    }

    void WeaponRotation()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 weaponPos = Camera.main.WorldToScreenPoint(chainSaw.transform.position);
        //Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        //lookPos = lookPos - chainSaw.transform.position;
        mousePos.x = mousePos.x - weaponPos.x;
        mousePos.y = mousePos.y - weaponPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //chainSaw.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        chainSaw.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void CollectSoul()
    {
        souls++;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Counselor")
        {
            health -= dmgTaken * Time.deltaTime;
        }
    }
}
