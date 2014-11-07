﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Bullet : MonoBehaviour
    {
        public Vector2 dir;
        public float speed;

        private float timer = 5f;

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {

        }

        void Update()
        {
            if (!Data.Paused)
            {
                transform.Translate(dir * speed * Time.deltaTime);
                timer -= Time.deltaTime;
                if (timer < 0)
                    Destroy(this.gameObject);
            }
        }
    }
}
