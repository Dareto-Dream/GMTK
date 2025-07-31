using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject[] maps;
    private int currentMapIndex = 0;

    private void Start()
    {
        ChangeMap(0);
    }

    public void ChangeMap(int mapIndex)
    {
        if (mapIndex < 0 || mapIndex >= maps.Length) return;

        foreach (var map in maps)
            map.SetActive(false);

        maps[mapIndex].SetActive(true);
        currentMapIndex = mapIndex;
    }
}