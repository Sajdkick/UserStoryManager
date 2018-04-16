using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListElement : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler{

    private Vector2 pointerOffset;
    private RectTransform canvasRectTransform;
    private RectTransform panelRectTransform;

    public Text text;
    public string dragTag;

    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.transform as RectTransform;
            panelRectTransform = transform as RectTransform;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(dragTag != "")
        {
            ListManager.DraggedObjectData draggedObject = new ListManager.DraggedObjectData();
            draggedObject.gameObject = gameObject;
            draggedObject.origin = gameObject.GetComponentInParent<ListManager>();
            draggedObject.tag = dragTag;
            ListManager.draggedObject = draggedObject;
            gameObject.transform.SetParent(canvasRectTransform);
            panelRectTransform.SetAsLastSibling();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if(dragTag != "")
        {
            if (panelRectTransform == null)
                return;

            Vector2 pointerPostion = ClampToWindow(data);

            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform, pointerPostion, data.pressEventCamera, out localPointerPosition
            ))
            {
                panelRectTransform.localPosition = localPointerPosition - pointerOffset;
            }
        }
    }
    Vector2 ClampToWindow(PointerEventData data)
    {
        Vector2 rawPointerPosition = data.position;

        Vector3[] canvasCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners(canvasCorners);

        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        Vector2 newPointerPosition = new Vector2(clampedX, clampedY);
        return newPointerPosition;
    }

    public void OnPointerUp(PointerEventData data)
    {
        if(dragTag != "")
        {
            if(ListManager.draggedObject != null && ListManager.draggedObject.gameObject == gameObject)
            {
                StartCoroutine(Dropped());
            }
        }
    }
    IEnumerator Dropped()
    {
        gameObject.GetComponent<RectTransform>().position += Vector3.up * 1000;
        yield return 0;
        yield return 0;

        if (ListManager.draggedObject != null)
        {
            ListManager.draggedObject.origin.AddToList(ListManager.draggedObject.gameObject);
            ListManager.draggedObject = null;
        }
    }
}
