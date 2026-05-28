using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 2f;

    private Camera _playerCamera;

    private void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {

        Vector3 centerOfCameraViewport = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = _playerCamera.ViewportPointToRay(centerOfCameraViewport);
        RaycastHit[] hits = Physics.RaycastAll(ray, _interactionDistance);

        foreach (var hitInfo in hits)
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable == null) continue;

            interactable.Interact();
        }


    }


}
