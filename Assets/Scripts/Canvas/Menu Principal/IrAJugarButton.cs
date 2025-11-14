using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IrAJugarButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(IrAJugar);
    }

    public void IrAJugar()
    {
        PoolManager.ClearAll();
        SceneManager.LoadScene(2);
    }
}