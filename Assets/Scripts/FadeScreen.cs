using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;
    public float time = 0f;
    public float fadeTime;

    public void Fade()
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        fadeImage.gameObject.SetActive(true);
        time = 0f;
        Color color = fadeImage.color;
      while(color.a <1f)
      {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(0,1,time);
            fadeImage.color = color;
            yield return null;
      }
      
      time = 0f;
      yield return new WaitForSeconds(0.15f);

      while(color.a > 0f)
      {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(1,0,time);
            fadeImage.color = color;
            yield return null;
      }
        fadeImage.gameObject.SetActive(false);
      yield return null;
    }
}
