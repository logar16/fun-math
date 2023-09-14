using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace FunMath
{
    public class FeedbackManager : MonoBehaviour
    {
        public Canvas worldCanvas;
        public GameObject OnHealthChangedObject;
        public float UIJumpDistance = 10f;
        private HealthCalculator[] HealthCalculators;
        public float FlashTiming = 1.0f;
        public float JumpTiming = 0.5f;

        public void OnEnable()
        {
            // Find all objects with HealthCalculator component
            HealthCalculators = GameObject.FindObjectsOfType<HealthCalculator>();
            // Listen to their event
            foreach (var healthCalculator in HealthCalculators)
            {
                healthCalculator.OnHealthChanged += OnHealthChanged;
            }
        }

        public void OnDisable()
        {
            foreach (var healthCalculator in HealthCalculators)
            {
                healthCalculator.OnHealthChanged -= OnHealthChanged;
            }
        }

        public void OnHealthChanged(OnHealthChangeData data)
        {
            Color targetColor;
            string targetColorStr;
            // Getting away from 0 means going green, going towards 0 means its red
            if (Mathf.Abs(data.ResultantHealth) < Mathf.Abs(data.PrevHealth))
            {
                // Health is going towards 0
                targetColor = Color.red;
                targetColorStr = "orange";
            }
            else
            {
                // Health is going away from 0
                targetColor = Color.green;
                targetColorStr = "orange";
            }

            if(data.ResultantHealth == 0)
            {
                targetColor = new Color(8, 73, 0); // Dark Green Color
                targetColorStr = "#0000FF";
            }

            // TargetColorStr is either dark green (dead) or orange (alive)

            // Spawn a prefab here
            if (OnHealthChangedObject)
            {
                var spawnedObject = GameObject.Instantiate(OnHealthChangedObject, data.modifiedGameObject.transform.position, Quaternion.identity, worldCanvas.transform);
                var TmP = spawnedObject.GetComponent<TMPro.TextMeshProUGUI>();

                // Determine string to show
                string displayString = "";
                displayString += "<color=\"green\">" + GetOperatorString(data.Operation) + "</color> <color=\"yellow\">" + data.Modifier.ToString() + 
                                "</color> = [<color=\"" + targetColorStr + "\">" + data.ResultantHealth.ToString() + "</color>]";
                // Show string
                TmP.text = displayString;
                // Start a coroutine that goes from target color to fade out
                StartCoroutine(ChangeColorOverTime(TmP, Color.white, Color.clear));
                // Start another coroutine that makes it 'jump up'
                // Find target position first
                Vector3 origPosition = data.modifiedGameObject.transform.position;
                Vector3 targetPosition = new Vector3(origPosition.x, origPosition.y + UIJumpDistance, origPosition.z);
                spawnedObject.transform.DOMoveY(targetPosition.y, JumpTiming).SetEase(Ease.OutExpo);
            }

            // Get the sprite renderer of the object hit to do a red flash (for hit)
            var spriteR = data.modifiedGameObject.GetComponent<SpriteRenderer>();
            if(spriteR)
            {
                // Change color to red
                spriteR.color = targetColor;
                // Start a couroutine to lerp the color from red to white
                StartCoroutine(ChangeColorOverTime(spriteR, targetColor, Color.white));
            }

        }
        // PERCENT
        public int operationStrSize = 150;
        public string GetOperatorString(OperationType operationType)
        {
            string str = "<size=" + operationStrSize.ToString() + "%>";
            switch(operationType)
            {
                case OperationType.Addition:
                    str += "+";
                    break;
                case OperationType.Subtraction:
                    str += "-";
                    break;
                case OperationType.Multiply:
                    str += "x";
                    break;
                case OperationType.Divide:
                    str += "÷";
                    break;
                default:
                    str += $"UNIMPLEMENTED {operationType.ToString()}";
                    break;
            }
            str += "</size>";
            return str;
        }


        IEnumerator ChangeColorOverTime(SpriteRenderer renderer, Color initialColor, Color targetColor)
        {
            float time = 0;
            while(time < FlashTiming)
            {
                time += Time.deltaTime;
                renderer.color = Color.Lerp(initialColor, targetColor, time / FlashTiming);
                yield return null;
            }
        }
        IEnumerator ChangeColorOverTime(TMPro.TextMeshProUGUI renderer, Color initialColor, Color targetColor)
        {
            float time = 0;
            while (time < FlashTiming)
            {
                time += Time.deltaTime;
                renderer.color = Color.Lerp(initialColor, targetColor, time / FlashTiming);
                yield return null;
            }
        }
        IEnumerator MoveOverTime(Transform transform, Vector3 initialPos, Vector3 targetPos)
        {
            float time = 0;
            while (time < JumpTiming)
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(initialPos, targetPos, time / JumpTiming);
                yield return null;
            }
        }
    }
}
