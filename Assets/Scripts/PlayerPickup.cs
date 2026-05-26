using TMPro;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{


    [SerializeField] private Transform _carriedObjectPosition;
    [SerializeField] private Transform _carriedObject;
    [SerializeField] private Transform _interactionTarget;
    [SerializeField] private Transform _interactionUI;
    [SerializeField] private BoxCollider _deliveryAreaCollider;
    [SerializeField] private TextMeshProUGUI _boxesDeliveredText;
    [SerializeField] private Transform _boxesDeliveredPopup;
    public int nrBoxesDelivered;

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

        if (Input.GetKeyDown(KeyCode.E))
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

                Collider[] colliders =
                    Physics.OverlapBox(_deliveryAreaCollider.transform.position + _deliveryAreaCollider.center, _deliveryAreaCollider.size * .5f);
                int previousBoxesDelivered = nrBoxesDelivered;
                nrBoxesDelivered = 0;
                foreach (Collider c in colliders)
                {
                    if (c.CompareTag("Box"))
                    {
                        nrBoxesDelivered++;
                    }
                }

                if (previousBoxesDelivered != nrBoxesDelivered)
                {
                    Instantiate(
                        _boxesDeliveredPopup,
                        transform.position + transform.forward * 2f,
                        Quaternion.identity);
                }

                _boxesDeliveredText.text = "Boxes Delivered: " + nrBoxesDelivered;
            }
        }
    }


}