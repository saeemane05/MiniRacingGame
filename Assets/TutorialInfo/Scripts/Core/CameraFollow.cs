using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 6f, -10f);
    public float followSpeed = 5f;
    public float rotationSpeed = 4f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition =
            target.position + target.rotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        Quaternion desiredRotation =
            Quaternion.LookRotation(target.position - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
