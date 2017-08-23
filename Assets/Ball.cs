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
        Bounding(collision);
    }

    private void Bounding(Collider2D collision)
    {
        var other = collision.gameObject.GetComponent<Ball>();
        if (other != null)
        {
            //Debug.Log("B!!");

            float dx = other.transform.localPosition.x - this.transform.localPosition.x;
            float dy = other.transform.localPosition.y - this.transform.localPosition.y;

            Vector2 bVect = new Vector2(dx, dy);

            float angle = Mathf.Atan2(bVect.y, bVect.x);

            var cosine = Mathf.Cos(angle);
            var sine = Mathf.Sin(angle);

            float targetX = Velocity.x + cosine * 1;
            float targetY = Velocity.y + sine * 1;
            float ax = (targetX);
            float ay = (targetY);

            dir = (new Vector2(-ax, -ay).normalized * Velocity.magnitude);
            dir1 = (new Vector2(ax, ay).normalized * Velocity.magnitude);

            other_p = other.transform.localPosition;
            this.ApplyForce(new Vector2(-ax, -ay).normalized * Velocity.magnitude);
            other.ApplyForce(new Vector2(ax, ay).normalized * Velocity.magnitude);
            //this.ApplyForce(Vector3.zero);
            //other.ApplyForce(Vector3.zero);
        }
    }

    Vector2 dir;
    Vector2 dir1;

    Vector3 other_p;
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.localPosition, dir);
        Gizmos.DrawLine(other_p, dir1);
    }
}
