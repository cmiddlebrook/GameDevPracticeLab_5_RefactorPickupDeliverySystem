using TMPro;
using UnityEngine;

public class DeliveryArea : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _boxesDeliveredText;
    [SerializeField] private Transform _boxesDeliveredPopup;
    
    private BoxCollider _dropAreaBoxCollider;
    public int nrBoxesDelivered;

    private void Awake()
    {
        _dropAreaBoxCollider = GetComponent<BoxCollider>();
    }

    public void HandleObjectDrop()
    {
        Collider[] colliders =
            Physics.OverlapBox(_dropAreaBoxCollider.transform.position + _dropAreaBoxCollider.center, _dropAreaBoxCollider.size * .5f);
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
            Instantiate(_boxesDeliveredPopup, transform.position, Quaternion.identity);
        }

        _boxesDeliveredText.text = "Boxes Delivered: " + nrBoxesDelivered;
    }
}
