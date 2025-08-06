using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject[] maps;
    private int currentMapIndex = 0;
    [SerializeField] private ScreenFader screenFader;


    public void Blacken()
    {
        StartCoroutine(screenFader.FadeOut());
    }

    public void Unblacken()
    {
        StartCoroutine(screenFader.FadeIn());
    }

    public void ChangeMap(int mapIndex)
    {
        if (mapIndex < 0 || mapIndex >= maps.Length) return;

        foreach (var map in maps)
            map.SetActive(false);

        maps[mapIndex].SetActive(true);
        currentMapIndex = mapIndex;
        Unblacken();
    }
}