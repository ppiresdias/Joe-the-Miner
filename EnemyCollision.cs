using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // This is very important if we want to restart the level

namespace UnityStandardAssets._2D
{
    public class EnemyCollision : MonoBehaviour
    {
        private EnemyController ec;
        private EnemyController side;
        public string hitWall = "Right";
        // Start is called before the first frame update
        void Start()
        {
            ec = GetComponent<EnemyController>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "Tilemap")
            {
                if (hitWall == "Left")
                {
                    Debug.Log("Right");
                    ec.hitWall = "Right";
                    hitWall = "Right";
                }
                else if(hitWall == "Right")
                {
                    Debug.Log("Left");
                    ec.hitWall = "Left";
                    hitWall = "Left";
                }
                Waiter();
            }
            //if (collision.gameObject.tag == "Weapon")
               if (collision.gameObject.tag == "Player")
            {
                //Debug.Log("Hit!");
                SceneManager.LoadScene("World1-1");//Scene name
                //Destroy(GetComponent<BoxCollider>());
                //Destroy(gameObject);
            }
        }

        IEnumerator Waiter()
        {
            yield return new WaitForSeconds(10);
            
        }
        void Update()
        {
            
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
           Debug.Log("Got contact");
            if(collision.contacts.Length > 0)
            {
                Debug.Log("Got here");
                ContactPoint2D contact = collision.contacts[0];
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    Debug.Log("Ground");
                    ec.isGrounded = true;
                }
                if(Vector3.Dot(contact.normal, Vector3.right) > 0.01)
                {
                    Debug.Log("Left");
                    ec.hitWall = "Left";
                    hitWall = ec.hitWall;
                }

                if(Vector3.Dot(contact.normal, Vector3.left) > 0.5)
                {
                    Debug.Log("Right");
                    ec.hitWall = "Right";
                    hitWall = ec.hitWall;
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Weapon")
            {
                Debug.Log("Hit!");
                Destroy(GetComponent<BoxCollider>());
                Destroy(gameObject);
            }
            // Debug.Log("Got contact");
            if(collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    //Debug.Log("Ground");
                    ec.isGrounded = true;
                }
                if(Vector3.Dot(contact.normal, Vector3.right) > 0.001)
                {
                    //Debug.Log("Left");
                    ec.hitWall = "Left";
                    hitWall = ec.hitWall;
                }

                if(Vector3.Dot(contact.normal, Vector3.left) > 0.5)
                {
                    //Debug.Log("Right");
                    ec.hitWall = "Right";
                    hitWall = ec.hitWall;
                }
            }
        }
        // Update is called once per frame

    }
}