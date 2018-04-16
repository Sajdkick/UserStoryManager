using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ListManager : MonoBehaviour, IPointerEnterHandler {

    public GameObject contentGameObject;
    public GameObject listElementPrefab;

    public static DraggedObjectData draggedObject;
    public string dropTag = "";

    [System.Serializable]
    public class ListEvent : UnityEvent<string> { }

    public ListEvent elementDropped;
    public ListEvent elementDragged;

    public void PopulateList(List<string> items, string dragTag = "")
    {
        ClearList();
        foreach(string item in items)
        {
            AddToList(item, dragTag);
        }
    }
    public void AddToList(string item, string dragTag = "")
    {
        GameObject newListElement = GameObject.Instantiate(listElementPrefab, Vector3.zero, Quaternion.identity);
        newListElement.transform.SetParent(contentGameObject.transform, false);
        newListElement.GetComponent<ListElement>().text.text = item;
        newListElement.GetComponent<ListElement>().dragTag = dragTag;
    }
    public void AddToList(GameObject item)
    {
        item.transform.SetParent(contentGameObject.transform, false);
    }
    public void ClearList()
    {
        while(contentGameObject.transform.childCount != 0)
        {
            Transform child = contentGameObject.transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    public class DraggedObjectData {

        public GameObject gameObject;
        public ListManager origin;
        public string tag;
    
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (draggedObject != null && !Input.GetMouseButton(0))
        {
            if (draggedObject.tag == dropTag)
            {
                AddToList(draggedObject.gameObject);
                string test = draggedObject.gameObject.GetComponent<ListElement>().text.text;
                draggedObject.origin.elementDragged.Invoke(draggedObject.gameObject.GetComponent<ListElement>().text.text);
                elementDropped.Invoke(draggedObject.gameObject.GetComponent<ListElement>().text.text);
                draggedObject = null;
            }
        }
    }

}
