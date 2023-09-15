using UnityEngine;
using UnityEngine.UI;

namespace FunMath
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void UpdateHealthBar(int currentValue, int maxValue)
        {
            slider.value = currentValue / maxValue;
        }
    }
}
