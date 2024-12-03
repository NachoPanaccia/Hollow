using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IrAInicioButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(IrAInicio);
    }

    public void IrAInicio()
    {
        SceneManager.LoadScene(0);
    }
}