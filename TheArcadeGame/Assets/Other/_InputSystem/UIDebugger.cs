using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIDebugger : MonoBehaviour
{
    private void Update()
    {
        // Get mouse position using the New Input System
        Vector2 mousePosition = Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;

        // Set up pointer event data
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        // Raycast against the UI
        List<RaycastResult> results = new List<RaycastResult>();
        if (EventSystem.current != null)
        {
            EventSystem.current.RaycastAll(eventData, results);
        }

        // Print results on Left Click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (results.Count > 0)
            {
                Debug.Log($"<b>[UI Debugger]</b> Clicked! Hits count: {results.Count}. Top element hit: <b>{results[0].gameObject.name}</b>", results[0].gameObject);

                for (int i = 0; i < results.Count; i++)
                {
                    Debug.Log($"   -> Layer [{i}]: {results[i].gameObject.name} (Canvas Order: {results[i].sortingOrder})");
                }
            }
            else
            {
                Debug.LogWarning("<b>[UI Debugger]</b> Clicked, but Raycast hit NOTHING. Ensure Event Camera is set (if not Overlay) and GraphicRaycaster is on the Canvas.");
            }
        }
    }
}