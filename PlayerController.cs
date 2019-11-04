using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace UnityStandardAssets._2D
{
    public class PlayerController : MonoBehaviour
    {
        public float horizontalSpeed = 0.3f;
        public float jumpForce = .08f;
        public bool isGrounded = true;
        public string hitWall = "None";
        public GameObject weapon;
        private Vector2 offset;
        private Vector2 fwd;
        private GameObject clone;

        private Rigidbody2D rb;
        private Animator anim;
        public float attackDelay = 3;
        private float savedAttackDelay;
        private bool facingRight = true;  // Determines the players current facing direction
        private bool done; //Set true if time is up
        private bool attacked;   //Player attacked recently
        // Use this for initialization
        
        private void Awake()
        {
            //Create component shorthand here
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            //Fill array
            //platforms = GameObject.FindGameObjectsWithTag("Ground");
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; //Makes it so velocity doesn't knock over player
            savedAttackDelay = attackDelay;
            weapon.GetComponent<Collider2D>().enabled = false; //Make the child attack object invisible

        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            //Debug.Log(isGrounded);
            
        }

        private void Update()
        {
            if(attacked == true)
            {
                attackDelay -= Time.deltaTime;
                Debug.Log(attackDelay);
                if(attackDelay <= 0)
                {
                    attacked = false;
                    attackDelay = savedAttackDelay;
                }
                if(attackDelay <= savedAttackDelay-1)
                {
                    weapon.GetComponent<Collider2D>().enabled = false; //Make the child attack object invisible
                }
            }
        }
        //Controls the players movement, calls other methods for each different movement type
        public void Move(float move, bool jump, bool attack, bool drop)
        {
            //Debug.Log(hitWall);
            //Move the player
            
            if(hitWall == "Left" && move < 0)
            {
                //Debug.Log("Dont move left");
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            
            else if(hitWall == "Right" && move > 0)
            {
                //Debug.Log("Dont move right");
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            else
            {
                rb.velocity = new Vector2(move * horizontalSpeed, rb.velocity.y);
            }
            
            
           // Debug.Log(rb.velocity.y);
            //The Speed animator parameter is set to the absolute value of the horizontal input
            //anim.SetFloat("Speed", Mathf.Abs(move));

            // If the player is able to bounce.
            if (jump == true && isGrounded == true)
            {
                Jump();

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

            if(attack == true)
            {
                //Goto attack method
                if(attacked == false)
                {
                    Attack();
                }
            }

            if(drop == true)
            {
                //Goto item method, player uses item
                Item();
            }
        }

        
        //Allows the character to bounce
        private void Jump()
        {

            //Add a vertical force to player and say they are not on the ground
            rb.AddForce(new Vector2(0f, jumpForce));
            isGrounded = false;
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

        private void Attack()
        {
            Debug.Log("Player Attacks!");
            weapon.GetComponent<Collider2D>().enabled = true; //Make the child attack object visible
            attacked = true;
        }

        private void Item()
        {
            Debug.Log("Player Uses Item");
        }
    }

    
}
