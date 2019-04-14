using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class ScoreManager : MonoBehaviour
    {
        public int score = 0;

        private AudioSource sound;

        void Start()
        {
            sound = GetComponent<AudioSource>();
        }

        void OnCollisionEnter(Collision other)
        {
            Ball hitBall = other.gameObject.GetComponent<Ball>();
            if(hitBall != null)
            {
                score++;
                sound.Play();
                Destroy(hitBall.gameObject);
            }
        }
    }
}