using UnityEngine;

public class Box : MonoBehaviour, ICarryable
{

    public void Pickup(Transform heldPosition)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = heldPosition;
        transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }

}
