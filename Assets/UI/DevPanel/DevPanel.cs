using UnityEngine;

public class DevPanel : MonoBehaviour
{
    public GameObject DevPanelGameObject;
    public bool ShowDevPanel;

    private void Start()
    {
        DevPanelGameObject.SetActive(ShowDevPanel);
    }
}
