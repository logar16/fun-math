using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace FunMath
{
    public class SceneManager : MonoBehaviour
    {
        public Image Fader;
        public int GameSceneIndex = 0;
        public float Rate = 1f;

        private Scene GameOverScene;

        public void ChangeToGame()
        {
            StartCoroutine(FadeToBlack());
        }

        IEnumerator FadeToBlack()
        {
            Debug.Log("Start fade");
            while (Fader.color.a < 1)
            {
                Debug.Log(Fader.color.a);
                yield return new WaitForEndOfFrame();
                Fader.color = new Color(0, 0, 0, Fader.color.a + (Rate * Time.deltaTime));
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        //IEnumerator GameOverSequence()
        //{

        //}
    }
}
