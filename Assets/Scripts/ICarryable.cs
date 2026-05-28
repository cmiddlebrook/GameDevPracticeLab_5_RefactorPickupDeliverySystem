
using UnityEngine;

public interface ICarryable 
{
    void Pickup(Transform heldPosition);
    void Drop();
}
