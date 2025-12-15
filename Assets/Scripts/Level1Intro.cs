using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
[DefaultExecutionOrder(-10000)]
public class Level1Intro : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text lineText;
    [SerializeField] private TMP_Text continueText;

    [Header("Texto")]
    [SerializeField] private string prefijoEstatico = "(Guía) — ";
    [TextArea(2, 6)] public string[] lineas;

    [Header("Animación")]
    [SerializeField] private float fadeIn = 0.35f;
    [SerializeField] private float fadeOut = 0.25f;
    [SerializeField] private float charsPorSegundo = 45f;
    [SerializeField] private float pausaPuntuacionFactor = 5f;

    // Estado interno
    private bool visible = false;
    private bool isTyping = false;
    private bool skipRequested = false;
    private int indice = 0;
    private int lastKeyProcessedFrame = -1;

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Awake()
    {
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            canvasGroup.transform.SetAsLastSibling();
        }

        if (continueText) continueText.gameObject.SetActive(false);
        if (lineText) lineText.text = "";
    }

    private void Start()
    {
        GameManager.SetInputLocked(true);
        Time.timeScale = 0f;

        // Fade in
        if (canvasGroup) StartCoroutine(FadeCanvas(canvasGroup, 0f, 1f, fadeIn));
        visible = true;

        if (lineas == null || lineas.Length == 0)
        {
            lineas = new[]
            {
                "Bienvenido, señor.",
                "Gracias por prestar sus servicios a la noble casa de los Black.",
                "Mi nombre, por si se le olvido, es Alfred; aun que no creo que nos veamos mucho por aqui.",
                "Su deber es simple: eliminar a todos los engendros que se originaron en esta casa.",
                "Puede moverse con W / A / S / D y atacarlos con su arma, presionando el click izquierdo.",
                "La noble casa de los Black agradece sus servicios, buen señor.",
                "Perdone, señor, una ultima cosa, quiero agradecerle al profesor, Juan Ignacio, por probar nuestro juego!"
            };
        }

        StartCoroutine(EscribirLineaActual());
    }

    private void Update()
    {
        if (!visible) return;

        if (continueText && !isTyping && continueText.gameObject.activeSelf)
        {
            var c = continueText.color;
            c.a = 0.5f + 0.5f * Mathf.Abs(Mathf.Sin(Time.unscaledTime * 3f));
            continueText.color = c;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.frameCount != lastKeyProcessedFrame)
        {
            lastKeyProcessedFrame = Time.frameCount;

            if (isTyping)
            {
                skipRequested = true;
            }
            else
            {
                Avanzar();
            }
        }
    }

    private void Avanzar()
    {
        if (indice < lineas.Length - 1)
        {
            indice++;
            if (continueText) continueText.gameObject.SetActive(false);
            StartCoroutine(EscribirLineaActual());
        }
        else
        {
            StartCoroutine(Cerrar());
        }
    }

    private IEnumerator EscribirLineaActual()
    {
        isTyping = true;
        skipRequested = false;

        string full = lineas[indice];
        if (lineText) lineText.text = prefijoEstatico;

        int i = 0;
        while (i < full.Length)
        {
            if (skipRequested)
            {
                if (lineText) lineText.text = prefijoEstatico + full;
                break;
            }

            if (full[i] == '<')
            {
                int cierre = full.IndexOf('>', i);
                if (cierre != -1)
                {
                    string tag = full.Substring(i, cierre - i + 1);
                    if (lineText) lineText.text += tag;
                    i = cierre + 1;
                    continue;
                }
            }

            char ch = full[i++];
            if (lineText) lineText.text += ch;

            float baseDelay = 1f / Mathf.Max(1f, charsPorSegundo);
            float delay = baseDelay;
            if (ch == '.' || ch == ',' || ch == ';' || ch == ':' || ch == '!' || ch == '?')
                delay *= pausaPuntuacionFactor;

            yield return new WaitForSecondsRealtime(delay);
        }

        isTyping = false;
        if (continueText) continueText.gameObject.SetActive(true);
    }

    private IEnumerator Cerrar()
    {
        visible = false;
        if (continueText) continueText.gameObject.SetActive(false);

        if (canvasGroup)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;

            yield return FadeCanvas(canvasGroup, canvasGroup.alpha, 0f, fadeOut);
        }

        Time.timeScale = 1f;
        GameManager.SetInputLocked(false);
        Destroy(gameObject);

        yield break;
    }

    private IEnumerator FadeCanvas(CanvasGroup cg, float from, float to, float dur)
    {
        float t = 0f;
        cg.alpha = from;
        while (t < dur)
        {
            t += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(from, to, t / dur);
            yield return null;
        }
        cg.alpha = to;
    }
}
