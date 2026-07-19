using UnityEngine;

public class Trash : MonoBehaviour
{
    private bool isDragging;
    private Vector3 offset;

    void OnMouseDown()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        offset = transform.position - mouse;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        transform.position = mouse + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}