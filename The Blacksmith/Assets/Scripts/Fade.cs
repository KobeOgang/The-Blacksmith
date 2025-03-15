using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 5.0f;
    [SerializeField] private GameObject fade;


    private void Update()
    {
        if (canvasGroup.alpha == 1)
        {   
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
            StartCoroutine(DisableInteractionsAfterFade());
        }
    }

    public void FadeIn()
    {
        canvasGroup.blocksRaycasts = true; // Allow interactions
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
        fade.SetActive(true);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
        StartCoroutine(DisableInteractionsAfterFade()); // Disable clicks after fade out
    }

    // Ensures the canvas is not clickable when faded out
    private IEnumerator DisableInteractionsAfterFade()
    {
        yield return new WaitForSeconds(fadeDuration);
        canvasGroup.blocksRaycasts = false;
    }
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedtime = 0.0f;
        while (elapsedtime < fadeDuration)
        {
            elapsedtime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedtime / duration);
            yield return null;
        }
    }
}
