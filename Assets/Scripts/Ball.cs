using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Rigidbody2D rigid;

    [SerializeField]
    float speed;

    public Vector2 Velocity
    {
        get
        {
            return rigid.velocity;
        }
    }

    // Use this for initialization
    void Start () {

        rigid = this.GetComponent<Rigidbody2D>();
        rigid.velocity = Random.insideUnitCircle.normalized * speed;// new Vector3(1.0f, 0.5f, 0);

        this.transform.localPosition = Random.insideUnitCircle * 1;

        this.transform.localScale = Vector3.one * (1 + Random.value * 1f);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void ApplyForce(Vector2 force)
    {
        rigid.velocity = force;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Bounding(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bouncing(collision);
    }

    private void Bouncing(Collider2D collision)
    {
        var other = collision.gameObject.GetComponent<Ball>();
        if (other != null)
        {
            float dx = other.transform.localPosition.x - this.transform.localPosition.x;
            float dy = other.transform.localPosition.y - this.transform.localPosition.y;

            Vector2 bVect = new Vector2(dx, dy);

            float angle = Mathf.Atan2(bVect.y, bVect.x);

            float targetX = this.transform.localPosition.x + Mathf.Cos(angle);
            float targetY = this.transform.localPosition.y + Mathf.Sin(angle);

            float ax = (targetX - other.transform.localPosition.x);
            float ay = (targetY - other.transform.localPosition.y);

            Vector2 vel = new Vector2(Velocity.x - ax, Velocity.y - ay);
            Vector2 other_vel = new Vector2(other.Velocity.x + ax, other.Velocity.y + ay);

            this.ApplyForce(vel.normalized * Velocity.magnitude);
            other.ApplyForce(other_vel.normalized * Velocity.magnitude);
        }
    }
}
