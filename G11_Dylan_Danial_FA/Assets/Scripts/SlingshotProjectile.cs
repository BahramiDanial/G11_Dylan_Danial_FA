using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlingshotProjectile : MonoBehaviour {
    public bool m_draggable = true; // Whether this object can be clicked and dragged with the mouse
    private bool m_isBeingDragged = false;

    [SerializeField] private GameObject m_slingshotAnchor = null;
    [SerializeField] private float m_maxSlingshotLength;

    private bool IsSelfClicked() {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject == this.gameObject) {
                    return true;
                }
            }
        }

        return false;
    }

    private void MoveToMousePosition(bool anchoredToSlingshot) {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Screen z = Camera worldspace z

        Vector3 newWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (anchoredToSlingshot) {
            Vector3 slingshotOffset = newWorldPosition - m_slingshotAnchor.transform.position;

            if (slingshotOffset.magnitude > m_maxSlingshotLength) {
                slingshotOffset = slingshotOffset.normalized * m_maxSlingshotLength;
            }

            transform.position = new Vector3(m_slingshotAnchor.transform.position.x + slingshotOffset.x, m_slingshotAnchor.transform.position.y + slingshotOffset.y, 0);
        }
        else {
            transform.position = new Vector3(newWorldPosition.x, newWorldPosition.y, 0);
        }
        
        Debug.Log("Mouse position: " + Input.mousePosition);
        Debug.Log("Target world position: " + newWorldPosition);
    }

    void Update() {
        if (IsSelfClicked()) {
            if (m_draggable && !m_isBeingDragged) {
                m_isBeingDragged = true;
            }
        }
        else if (m_isBeingDragged && Input.GetMouseButtonUp((int)MouseButton.LeftMouse)) {
            m_isBeingDragged = false;
        }

        if (m_isBeingDragged) {
            MoveToMousePosition(true);
        }
    }
}
