using UnityEngine;
using static UnityEditor.Progress;

namespace FunMath
{
    public class ModifierObject : MonoBehaviour
    {
        [SerializeField]
        private int Modifier = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                var selector = player.ModifierSelector;
                if (selector != null)
                {
                    selector.AddItem(new ModifierItem(Modifier));
                    Destroy(gameObject);
                }
            }
        }
    }
}
