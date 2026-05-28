using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{

    [SerializeField] private Transform _carriedObjectPosition;
    [SerializeField] private ICarryable _carriedObject;
    [SerializeField] private ICarryable _interactionTarget;
    [SerializeField] private GameObject _interactionUI;
    [SerializeField] private TMP_Text _interactionText;
    [SerializeField] private DeliveryArea _deliveryArea;

    private Camera _playerCamera;
    private float _interactionDistance = 2f;

    private void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        SearchForItemsToPickup();
        HandleItemPickup();
    }


    private void SearchForItemsToPickup()
    {
        _interactionTarget = null;

        HideUI();

        Vector3 centerOfCameraViewport = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = _playerCamera.ViewportPointToRay(centerOfCameraViewport);
        RaycastHit[] hits = Physics.RaycastAll(ray, _interactionDistance);

        foreach (var hitInfo in hits)
        {
            ICarryable carryable = hitInfo.collider.GetComponent<ICarryable>();
            if (carryable == null) continue;

            _interactionTarget = carryable;
            ShowUI();
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
        _interactionTarget.Pickup(_carriedObjectPosition);
        _carriedObject = _interactionTarget;
    }

    private void DropItem()
    {
        _carriedObject.Drop();
        _carriedObject = null;

        _deliveryArea.HandleObjectDrop();
    }

    private void HideUI()
    {
        _interactionUI.SetActive(false);
    }

    private void ShowUI()
    {
        _interactionUI.SetActive(true);
        _interactionText.text = _carriedObject == null ? "Pickup" : "Drop";
    }


}