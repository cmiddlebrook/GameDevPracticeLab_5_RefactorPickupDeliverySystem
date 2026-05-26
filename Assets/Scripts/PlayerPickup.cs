using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Transform _carriedObjectPosition;
    [SerializeField] private Transform _carriedObject;
    [SerializeField] private Transform _interactionTarget;
    [SerializeField] private Transform _interactionUI;
    [SerializeField] private DeliveryArea _deliveryArea;

    private void Update()
    {
        _interactionTarget = null;

        _interactionUI.gameObject.SetActive(false);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit raycastHit, 3f))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out Box box))
            {
                // It's a delivery box
                _interactionTarget = raycastHit.transform;
                _interactionUI.gameObject.SetActive(true);

            }

        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_carriedObject == null)
            {
                // Not carrying anything, pick up
                if (_interactionTarget != null)
                {
                    _interactionTarget.GetComponent<Rigidbody>().isKinematic = true;
                    _interactionTarget.parent = _carriedObjectPosition;
                    _interactionTarget.localPosition = Vector3.zero;
                    _carriedObject = _interactionTarget;
                }
            }
            else
            {
                // Carrying something, drop it
                _carriedObject.GetComponent<Rigidbody>().isKinematic = false;
                _carriedObject.parent = null;
                _carriedObject = null;

                _deliveryArea.HandleObjectDrop();
            }
        }
    }


}