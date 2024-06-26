using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Unit
{
    public Rigidbody2D m_Rigidbody;
    public bool DESTORY;
    public GameObject partical;
    public override void OnDie(Collider2D collision)
    {
        ScoreManager.instance.uiScore = ScoreManager.instance.findscore(20);
        Destroy(collision.gameObject);
        Instantiate(partical, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        // 죽었을때 이팩트
        // 죽었으때 사운드
    }
    public static Meteor Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(speed());
        StartCoroutine(stop());
    }
    // Start is called before the first frame update
    void Start()
    {
        type = UnitType.Enemy;
        hp = MonsterHpManager.instance.meteor;
        maxHp = MonsterHpManager.instance.meteor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator stop()
    {
        yield return new WaitUntil(() => transform.position.y <= -4.36f);
        Destroy(gameObject);
    }
    IEnumerator speed()
    {
        DESTORY = true;
        m_Rigidbody.gravityScale = 0;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(5);
        DESTORY = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        m_Rigidbody.gravityScale = 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        TriggerManager.instance.GAM(gameObject);
        TriggerManager.instance.OnTriggerEnter2D(collision);//플레이어 총알 사라지게 하기
    }
}
