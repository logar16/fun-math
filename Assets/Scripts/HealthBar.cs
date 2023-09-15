using UnityEngine;
using UnityEngine.UI;

namespace FunMath
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void UpdateHealthBar(int currentValue, int maxValue)
        {
            slider.value = (float) currentValue / maxValue;
            Debug.Log($"Current value: {currentValue}, Max value: {maxValue}. percentage: {slider.value}");
        }
    }
}
