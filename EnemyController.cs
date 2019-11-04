using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace UnityStandardAssets._2D
{
    public class EnemyController : MonoBehaviour
    {
        public float horizontalSpeed = 0.05f;
        public float jumpForce = .08f;
        public int move = -1;
        public bool isGrounded = true;
        public string hitWall = "Right";
        private Vector2 offset;
        private Vector2 fwd;
        private GameObject clone;

        private Rigidbody2D rb;
        private Animator anim;
        private bool facingRight = true;  // Determines the players current facing direction
        
        private void Awake()
        {
            //Create component shorthand here
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            //Fill array
            //platforms = GameObject.FindGameObjectsWithTag("Ground");
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; //Makes it so velocity doesn't knock over player

        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            //Debug.Log(isGrounded);
            
        }

        private void Update()
        {
            rb.velocity = new Vector2(move * horizontalSpeed, rb.velocity.y);
            //Debug.Log(move);
            if(hitWall == "Left")
            {
                //Debug.Log("Left");
                move = 1;
            }
            
            else if(hitWall == "Right")
            {
                //Debug.Log("Right");
                move = -1;
            }

            //If the input is moving the player right and the player is facing left...
            if (move > 0 && !facingRight)
            {

                // ... flip the player.
                Flip();

            }

            //Otherwise if the input is moving the player left and the player is facing right...
            if (move < 0 && facingRight)
            {

                //Flips the player's avatar
                Flip();

            }
        }
        
        //Flips sprite when changing direction
        private void Flip()
        {

            //Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            //Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }
    }

    
}
