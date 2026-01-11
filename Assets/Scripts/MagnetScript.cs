using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    private Vector3 offset;

    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.parent.position).z;
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        offset = transform.parent.position - mouseWorldPos;
    }
    void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.parent.position).z;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.parent.position = mouseWorldPos + offset;
    }
}