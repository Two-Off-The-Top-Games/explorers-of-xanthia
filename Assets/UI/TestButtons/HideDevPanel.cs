using UnityEngine;

public class HideDevPanel : MonoBehaviour
{
    public bool ShowDevPanel;

    private void Start()
    {
        gameObject.SetActive(ShowDevPanel);
    }
}
