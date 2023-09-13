using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class Arrow : MonoBehaviour
    {
        public float speed = 20f;
        public Rigidbody2D arrowRigidBody;

        // Start is called before the first frame update
        void Start()
        {
            // Fly forward
            arrowRigidBody.velocity = transform.right * speed;
        
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.name);
        }
    }
}
