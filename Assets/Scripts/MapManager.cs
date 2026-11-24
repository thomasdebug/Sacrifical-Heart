using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject mapPanel;

    private bool isMapOpen = false;

    private void Start()
    {
        mapPanel.SetActive(false);
    }

    public void ToggleMap()
    {
        isMapOpen = !isMapOpen;
        mapPanel.SetActive(isMapOpen);
    }

    public void CloseMap()
    {
        isMapOpen = false;
        mapPanel.SetActive(false);
    }
}
