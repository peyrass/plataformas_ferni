using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Collider2D grabCollider;
    private bool canDrag = false;

    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (grabCollider.OverlapPoint(mouseWorldPos))
        {
            canDrag = true;
            offset = transform.position - mouseWorldPos;
        }
        else
        {
            canDrag = false;
        }
    }

    void OnMouseDrag()
    {
        if (!canDrag) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = mouseWorldPos + offset;
    }
}