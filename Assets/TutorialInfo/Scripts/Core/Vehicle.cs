using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    protected float distanceCovered;
    public float DistanceCovered => distanceCovered;

    public abstract void Move();
}
