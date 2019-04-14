using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Ball : MonoBehaviour
    {
        public float stopSpeed = 0.2f;
        
        private Rigidbody rigid;
        private AudioSource sound;

        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            sound = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 vel = rigid.velocity;

            // Check if velocity is going up
            if(vel.y > 0)
            {
                // Cap velocity
                vel.y = 0;
            }

            // If the velocity's speed is less than the stop speed
            if(vel.magnitude < stopSpeed)
            {
                // Cancel out velocity
                vel = Vector3.zero;
            }

            rigid.velocity = vel;
        }
        
        // Perform physics impact 
        public void Hit(Vector3 dir, float impactForce)
        {
            rigid.AddForce(dir * impactForce, ForceMode.Impulse);
        }

        // Checks if the magnitude is below the speed
        public bool IsStopped()
        {
            return rigid.velocity.magnitude <= 0.2f;
        }

        void OnCollisionEnter(Collision col)
        {
            Ball ball = col.gameObject.GetComponent<Ball>();
            if(ball != null)
            {
                sound.Play();
            }
        }
    }
}