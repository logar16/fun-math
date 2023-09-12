using UnityEngine;

namespace FunMath
{
    public class CharacterMovement : MonoBehaviour
    {
        public PlayerController controller;

        public float runSpeed = 40f;

        float horizontalMove = 0f;
        bool jump = false;

        // Update is called once per frame
        void Update()
        {

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }

        void FixedUpdate()
        {
            // Move our character
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        }
    }
}
