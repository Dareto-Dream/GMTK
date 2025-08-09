using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDebug : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData) => Debug.Log(gameObject.name + " OnPointerClick");
    public void OnPointerEnter(PointerEventData eventData) => Debug.Log(gameObject.name + " OnPointerEnter");
    public void OnPointerExit(PointerEventData eventData) => Debug.Log(gameObject.name + " OnPointerExit");
}
