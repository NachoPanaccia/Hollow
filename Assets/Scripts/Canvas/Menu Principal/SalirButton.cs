using UnityEngine;
using UnityEngine.UI;

public class SalirButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Salir);
    }

    public void Salir()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }
}