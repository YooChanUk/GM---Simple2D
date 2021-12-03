using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator ani;
    SpriteRenderer sprite;
    BoxCollider2D boxcollider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
        Invoke("Think",5);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.4f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("platform"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    //營敝л熱
    void Think()
    {
        nextMove = Random.Range(-1,2);

        ani.SetInteger("WalkSpeed", nextMove);
        //瞳 寞щ
        if (nextMove != 0)
        {
            sprite.flipX = nextMove == 1;
        }
        
        //營敝л熱
        float nextThinkTime = Random.Range(2f, 6f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        sprite.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }

    public void OnDamaged()
    {
        sprite.color = new Color(1,1,1,0.4f);

        sprite.flipY = true;

        boxcollider.enabled = false;

        rigid.AddForce(Vector2.up * 5,ForceMode2D.Impulse);

        Invoke("DeActive",5);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
