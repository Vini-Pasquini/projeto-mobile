using UnityEngine;
using UnityEngine.EventSystems;

public interface IDraggable
{
    public void OnDragStart();
    public void OnDragUpdate(PointerEventData eventData);
    public void OnDragEnd();
}
