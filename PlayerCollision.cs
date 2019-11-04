using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlayerCollision : MonoBehaviour
    {
        bool gem = false;
        private PlayerController pc;
        // Use this for initialization
        void Start()
        {
            pc = GetComponent<PlayerController>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "Enemy") //Tag any enemy with Enemy
            {
                collision.gameObject.SendMessage("GemCollect", -1); //Clears gems currently held
                collision.gameObject.SendMessage("TakeDamage", 1); //Player is killed
            } 
            
           // Debug.Log("Got contact");
            if(collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    //Debug.Log("Ground");
                    pc.isGrounded = true;
                }
                if(Vector3.Dot(contact.normal, Vector3.right) > 0.5)
                {
                    //Debug.Log("Left");
                    pc.hitWall = "Left";
                }

                else if(Vector3.Dot(contact.normal, Vector3.left) > 0.5)
                {
                    //Debug.Log("Right");
                    pc.hitWall = "Right";
                }

                else
                {
                    pc.hitWall = "None";
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            //Debug.Log(collision.collider.name);
            if(collision.gameObject.tag == "Gem") //Tag any collectable gems with Gem tag
            {
                collision.gameObject.SendMessage("GemCollect",1); //Increments the current gem amount by one
            }

            if (collision.gameObject.tag == "Cart") //Tag the cart with Cart
            {
                collision.gameObject.SendMessage("GemCollect", -1); //Clears gems currently held
                collision.gameObject.SendMessage("TotalGems", 1); //Tells TotalGems to add all current gems to total
            }
            // Debug.Log("Got contact");
            if(collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    //Debug.Log("Ground");
                    pc.isGrounded = true;
                }

                if(Vector3.Dot(contact.normal, Vector3.down) > 0.5)
                {
                    //Debug.Log("Ground");
                    pc.isGrounded = false;
                }

                if(Vector3.Dot(contact.normal, Vector3.right) > 0.5)
                {
                    //Debug.Log("Left");
                    pc.hitWall = "Left";
                }

                else if(Vector3.Dot(contact.normal, Vector3.left) > 0.5)
                {
                    //Debug.Log("Right");
                    pc.hitWall = "Right";
                }

                else
                {
                    pc.hitWall = "None";
                }
            }
            
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
