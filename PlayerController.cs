using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float jumpForce = 7f;
        private int jumpCount = 0;
        private bool isGrounded = false;

        private Rigidbody2D body;
        private SpriteRenderer sprite;
        private Animator animator;

        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            if (body == null)
            {
                Debug.LogError("Rigidbody2D tidak ditemukan pada objek ini!");
            }

            body.gravityScale = 2f;
            body.freezeRotation = true;
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (body == null) return;

            float move = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.D))
            {
                body.linearVelocity = new Vector2(speed, body.linearVelocity.y);
                sprite.flipX = false;
                animator.SetBool("isjumping", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                body.linearVelocity = new Vector2(-speed, body.linearVelocity.y);
                sprite.flipX = true;
                animator.SetBool("isjumping", true);
            }
            else
            {
                animator.SetBool("isjumping", false);
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                body.linearVelocity = new Vector2(0, body.linearVelocity.y);
            }

            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                jumpCount++;
                animator.SetBool("isjumping", true);
            }

        }

        void FixedUpdate()
        {
            if (body.linearVelocity.y < 0)
            {
                body.linearVelocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.fixedDeltaTime;
            }

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                jumpCount = 0;
                animator.SetBool("isjumping", false);
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    }
}


