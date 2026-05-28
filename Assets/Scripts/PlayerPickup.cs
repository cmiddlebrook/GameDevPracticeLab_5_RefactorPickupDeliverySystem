using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Transform _carriedObjectPosition;
    [SerializeField] private Transform _carriedObject;
    [SerializeField] private Transform _interactionTarget;
    [SerializeField] private Transform _pickupInteractionUI;
    [SerializeField] private TMP_Text _interactionText;
    [SerializeField] private DeliveryArea _deliveryArea;

    private void Update()
    {
        SearchForItemsToPickup();
        HandleItemPickup();
    }

    private void SearchForItemsToPickup()
    {
        _interactionTarget = null;

        HideUI();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit raycastHit, 3f))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out ICarryable carryable))
            {
                _interactionTarget = raycastHit.transform;
                ShowUI();
            }

        }
    }

    private void HandleItemPickup()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_carriedObject == null)
            {
                if (_interactionTarget != null)
                {
                    PickupItem();
                }
            }
            else
            {
                DropItem();
            }
        }
    }

    private void PickupItem()
    {
        _interactionTarget.GetComponent<Rigidbody>().isKinematic = true;
        _interactionTarget.parent = _carriedObjectPosition;
        _interactionTarget.localPosition = Vector3.zero;
        _carriedObject = _interactionTarget;
    }

    private void DropItem()
    {
        _carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        _carriedObject.parent = null;
        _carriedObject = null;

        _deliveryArea.HandleObjectDrop();
    }

    private void HideUI()
    {
        _pickupInteractionUI.gameObject.SetActive(false);
    }

    private void ShowUI()
    {
        _pickupInteractionUI.gameObject.SetActive(true);
        _interactionText.text = _carriedObject == null ? "Pickup" : "Drop";
    }


}