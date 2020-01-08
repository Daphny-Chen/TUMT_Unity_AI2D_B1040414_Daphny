using UnityEngine;                  // 引用 Unity API - API 倉庫 功能、工具
using UnityEngine.Events;           // 引用 事件 API
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int speed = 50;                  // 整數
    public float jump = 2.5f;               // 浮點數
    public string foxName = "主角";       // 字串
    public bool pass = false;               // 布林值 - true/false
    public bool isGround;

    public UnityEvent onEat;
    public AudioClip soundProp;

    private Rigidbody2D r2d;
    private AudioSource aud;

    [Header("血量"),Range(0,200)]
    public float hp = 100;

    public Image hpBar;
    private float hpMax;
    public GameObject flnal;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();

        hpMax = hp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn();
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
    }

    private void FixedUpdate()
    {
        Walk(); // 呼叫方法
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        //Debug.Log("碰到東西：" + collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "小星星")
        {
            Destroy(collision.gameObject);  // 刪除
            onEat.Invoke();                 // 呼叫事件
        }
        if (collision.tag == "胡蘿蔔")
        {
            Destroy(collision.gameObject);  // 刪除
            hp += 20;
            hpBar.fillAmount = hp / hpMax;
        }
    }
    /// <summary>
    /// 走路
    /// </summary>
    private void Walk()
    {
        if (r2d.velocity.magnitude < 20 && isGround)
            r2d.AddForce(new Vector2(speed * Input.GetAxisRaw("Horizontal"), 0));
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
        }
    }


    // 參數語法：類型 名稱
    /// <summary>
    /// 轉彎
    /// </summary>
    /// <param name="direction">方向，左轉為 180，右轉為 0</param>
    private void Turn(int direction = 0)
    {
        transform.eulerAngles = new Vector3(0, direction, 0);
    }


    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;


        if (hp <= 0)  flnal.SetActive(true);
    }


}
