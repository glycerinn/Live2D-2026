using UnityEngine;

public class DragBomb : MonoBehaviour
{
    private Bomb bomb;
    private bool isDragging;
    private Vector3 offset;

    void Awake()
    {
        bomb = GetComponent<Bomb>();
    }

    void OnMouseDown()
    {
        if (!bomb.IsDefused)
            return;

        isDragging = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        offset = transform.position - mousePos;
    }

    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        transform.position = mousePos + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}