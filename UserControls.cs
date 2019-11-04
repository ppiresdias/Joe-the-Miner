using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlayerController))]
    public class UserControls : MonoBehaviour
    {

        private PlayerController player;
        private bool bounce;
        private bool attack;
        private bool drop;
        // Use this for initialization
        void Awake()
        {
            player = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            // Read the jump input in Update so button presses aren't missed.
            bounce = Input.GetKey(KeyCode.Z);
        }

        void FixedUpdate()
        {
            // Read the inputs.
           // bool slide = Input.GetKey(KeyCode.S);
           // bool attack = Input.GetKey(KeyCode.Q);
            float h = Input.GetAxis("Horizontal");
            attack = Input.GetKey(KeyCode.X);
            drop = Input.GetKey(KeyCode.C);
            //Debug.Log(h);
            // Pass all parameters to the player controller script.
            player.Move(h, bounce, attack, drop);
            bounce = false;
            attack = false;
            drop = false;
        }
    }
}
