using System;
using UnityEngine;
using UnityEngine.Events;

namespace FunMath
{
    public class PlayerController : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject arrowPrefab;

        private InventorySelector<OperationItem> operationInventory = new InventorySelector<OperationItem>(4);
        private InventorySelector<ModifierItem> modifierInventory = new InventorySelector<ModifierItem>(5);

        [Header("Movement")]
        [Space]

        [SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
        [Range(0, .3f)][SerializeField] private float movementSmoothing = .05f;   // How much to smooth out the movement
        [SerializeField] private bool airControl = false;                         // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.

        const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded;            // Whether or not the player is grounded.
        private Rigidbody2D rigidBody;
        private bool facingRight = true;  // For determining which way the player is currently facing.
        private Vector3 velocity = Vector3.zero;

        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        public InventorySelector<OperationItem> OperationSelector
        {
            get { return operationInventory; }
        }

        public InventorySelector<ModifierItem> ModifierSelector
        {
            get { return modifierInventory; }
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            operationInventory.AddItem(new OperationItem(OperationType.Subtraction, 99));
            operationInventory.AddItem(new OperationItem(OperationType.Addition, 2));
            operationInventory.AddItem(new OperationItem(OperationType.Divide, 2));
            operationInventory.AddItem(new OperationItem(OperationType.Multiply, 2));
            modifierInventory.AddItem(new ModifierItem(1, 99));
            modifierInventory.AddItem(new ModifierItem(2, 3));
            modifierInventory.AddItem(new ModifierItem(3, 2));
            modifierInventory.AddItem(new ModifierItem(4, 1));
            modifierInventory.AddItem(new ModifierItem(5, 0));
        }

        void Update()
        {
            CheckInventorySelectionChange();
        }

        private void CheckInventorySelectionChange()
        {
            // Modifier Selection (use 1-5 keys)
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                modifierInventory.SelectIndex(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                modifierInventory.SelectIndex(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                modifierInventory.SelectIndex(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                modifierInventory.SelectIndex(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                modifierInventory.SelectIndex(4);
            }

            // Operation Selection (use Q and E keys)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                operationInventory.SwitchItemLeft();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                operationInventory.SwitchItemRight();
            }
        }

        private void FixedUpdate()
        {
            bool wasGrounded = grounded;
            grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
        }

        public void Move(float move, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (grounded || airControl)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, rigidBody.velocity.y);
                // And then smoothing it out and applying it to the character
                rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (grounded && jump)
            {
                // Add a vertical force to the player.
                grounded = false;
                rigidBody.AddForce(new Vector2(0f, jumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // rotate the player
            transform.Rotate(0f, 180f, 0f);
        }

        /// <summary>
        /// Used by the animation
        /// </summary>
        private void ShootArrow()
        {
            if (CheckArrowData())
            {
                SetArrowData();
                Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            }
        }

        public bool CheckArrowData()
        {
            // Load current operator and modifier
            OperationItem operationItem = OperationSelector.QueryCurrentItem();
            ModifierItem modifierItem = ModifierSelector.QueryCurrentItem();

            if (operationItem.Count == 0 || modifierItem.Count == 0)
            {
                // TODO: Highlight the item in HUD if count is 0.
                Debug.Log("Cannot attack because current operation count or modifier count is 0.");
                Debug.Log($"operation item count: {operationItem.Count}");
                Debug.Log($"modifier item count: {modifierItem.Count}");

                return false;
            }

            return true;
        }

        private void SetArrowData()
        {
            // Load current operator and modifier
            OperationItem operationItem = OperationSelector.QueryCurrentItem();
            ModifierItem modifierItem = ModifierSelector.QueryCurrentItem();

            OperationType operation = operationItem.Operator;
            int modifier = modifierItem.Modifier;

            // reduce item count
            OperationSelector.UseCurrentItem();
            ModifierSelector.UseCurrentItem();

            Arrow arrow = arrowPrefab.GetComponent<Arrow>();
            arrow.Operator = operation;
            arrow.Modifier = modifier;

            Debug.Log($"operation: {operation}");
            Debug.Log($"modifier: {modifier}");
        }
    }
}
