using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddle_movement : MonoBehaviour
{
    public Rigidbody2D rb;
    private float speed = 18f;
    public Vector2 direction;
    public float maxBounceAngle = 30f;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            direction = Vector2.left;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction = Vector2.right;
        } else {
            direction = Vector2.zero;
        }

        if (direction != Vector2.zero) {
            rb.AddForce(direction * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ball_movement ball = collision.gameObject.GetComponent<ball_movement>();

        if (ball != null)
        {
            Vector2 paddlePosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float maxOffset = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.GetComponent<ball_movement>().rb.velocity);
            float bounceAngle = (offset / maxOffset) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.GetComponent<ball_movement>().rb.velocity = rotation * Vector2.up * ball.GetComponent<ball_movement>().rb.velocity.magnitude;
        }
    }
}
