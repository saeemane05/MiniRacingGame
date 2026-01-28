using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCar : MonoBehaviour
{
    public float acceleration = 25f;
    public float turnSpeed = 120f;
    public float maxSpeed = 25f;   // 🔒 speed cap (IMPORTANT)

    private Rigidbody rb;
    private float distanceCovered;

    public float DistanceCovered => distanceCovered;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distanceCovered = 0f;
    }

    void FixedUpdate()
    {
        // 🛑 Stop completely when race finished
        if (RaceManager.Instance != null && RaceManager.Instance.IsRaceFinished)
            return;

        // 🛑 Stop when paused
        if (Time.timeScale == 0f)
            return;

        float move = 0f;
        float turn = 0f;

        if (Keyboard.current.wKey.isPressed)
            move = 1f;
        else if (Keyboard.current.sKey.isPressed)
            move = -1f;

        if (Keyboard.current.aKey.isPressed)
            turn = -1f;
        else if (Keyboard.current.dKey.isPressed)
            turn = 1f;

        // 🚗 Forward force
        rb.AddForce(transform.forward * move * acceleration, ForceMode.Acceleration);

        // 🔄 Turning (only while moving)
        if (Mathf.Abs(move) > 0.1f)
        {
            Quaternion turnRotation =
                Quaternion.Euler(0f, turn * turnSpeed * Time.fixedDeltaTime, 0f);

            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // 🛑 Clamp speed to prevent physics explosion
        Vector3 vel = rb.linearVelocity;
        vel.y = 0f;

        if (vel.magnitude > maxSpeed)
            rb.linearVelocity = vel.normalized * maxSpeed;

        // 📏 Distance tracking (correct & stable)
        float speedAlongForward =
            Vector3.Dot(rb.linearVelocity, transform.forward);
        if (speedAlongForward > 0f)
        {
            float deltaDistance = speedAlongForward * Time.fixedDeltaTime;
            distanceCovered += deltaDistance;
        }

    }
}
