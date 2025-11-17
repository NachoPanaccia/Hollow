using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private float flashDuration = 0.15f;
    [SerializeField] private float maxAlpha = 0.35f;

    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (flashImage == null)
        {
            flashImage = GetComponent<Image>();

        }
        SetAlpha(0f);
    }

    public void Flash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);

        }
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        SetAlpha(maxAlpha);

        float t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            float normalized = t / flashDuration;
            float alpha = Mathf.Lerp(maxAlpha, 0f, normalized);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0f);
        flashCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        if (flashImage == null) return;

        Color c = flashImage.color;
        c.a = alpha;
        flashImage.color = c;
    }
}
