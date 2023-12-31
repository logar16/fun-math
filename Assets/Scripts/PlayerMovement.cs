using UnityEngine;

namespace FunMath
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerController controller;
        public Animator animator;

        public float runSpeed = 40f;

        float horizontalMove = 0f;
        bool jump = false;

        public void OnLanding()
        {
            animator.SetBool("IsJumping", false);
        }

        // Update is called once per frame
        void Update()
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }

            if (Input.GetMouseButtonDown(0) && controller.CheckArrowData())
            {
                animator.SetBool("IsAttacking", true);

            }
        }

        void FixedUpdate()
        {
            // Move our character
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        }

        void OnAttackAnimationFinishTrigger()
        {
            animator.SetBool("IsAttacking", false);
        }
    }
}
