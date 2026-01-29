using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace PlatfromMania.UI
{
    public class MessageView : MonoBehaviour, IMessageView
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private CanvasGroup canvasGroup;
        public void ShowMessage(string message, float dureation)
        {
            StopAllCoroutines();
            StartCoroutine(ShowMessageCoroutine(message, dureation));
        }

        private IEnumerator ShowMessageCoroutine(string message, float duration)
        {
            messageText.text = message;
            canvasGroup.alpha = 1f;

            yield return new WaitForSeconds(duration);

            canvasGroup.alpha = 0f;
        }
    }
}

