using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    public Text line1;
    public Text line2;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
