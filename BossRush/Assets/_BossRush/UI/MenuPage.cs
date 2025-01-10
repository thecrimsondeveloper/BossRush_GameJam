using UnityEngine;

public class MenuPage : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }


}
