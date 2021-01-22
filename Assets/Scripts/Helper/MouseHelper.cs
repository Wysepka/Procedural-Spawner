using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Mouse helper
public static class MouseHelper
{
    public static List<RaycastResult> ReturnPointerRaycastResults()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results;
    }

    public static bool IsPointerOverUIElement()
    {
        List<RaycastResult> results = ReturnPointerRaycastResults();
        return results.Count > 0;
    }

    public static bool IsPointerOverUIElement(string tag)
    {
        List<RaycastResult> results = ReturnPointerRaycastResults();
        foreach (RaycastResult item in results)
        {
            if (item.gameObject.CompareTag(tag)) return true;
        }

        return false;
    }


}
