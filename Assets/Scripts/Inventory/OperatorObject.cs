using UnityEngine;

namespace FunMath
{
    public class OperatorObject : MonoBehaviour
    {
        [SerializeField]
        private OperationType operation;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                var selector = player.GetOperationSelector();
                if (selector != null)
                {
                    selector.AddItem(new OperationItem(operation));
                    Destroy(gameObject);
                }
            }
        }

        //public void Pickup()
        //{
        //    // Destroying the object destroys reference in C#. The 'right'
        //    // move is to abstract out the item from monobehavior so it can live without a gameobject
        //    // Quick fix is to move this object to a place the player can never find
        //    transform.position = new Vector3(9999, 9999, 9999);
        //}
    }
}
