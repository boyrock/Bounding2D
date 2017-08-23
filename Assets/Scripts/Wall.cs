using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public Direction direction;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Bouncing(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bouncing(collision);
    }

    private void Bouncing(Collider2D collision)
    {
        var ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            var force = Vector3.zero;

            var velocity = ball.Velocity;
            switch (direction)
            {
                case Direction.Top:
                    force = new Vector2(velocity.x, -Mathf.Abs(velocity.y));
                    break;
                case Direction.Bottom:
                    force = new Vector2(velocity.x, Mathf.Abs(velocity.y));
                    break;
                case Direction.Left:
                    force = new Vector2(Mathf.Abs(velocity.x), velocity.y);
                    break;
                case Direction.Right:
                    force = new Vector2(-Mathf.Abs(velocity.x), velocity.y);
                    break;
                default:
                    break;
            }

            ball.ApplyForce(force);
        }
    }

    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }
}

