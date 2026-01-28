using UnityEngine;

public class AICar : Vehicle
{
    public float forwardSpeed = 14f;
    public float pushForce = 2f;
    public float pushDistance = 1.5f;

    private Rigidbody rb;
    private Rigidbody playerRb;
    private Transform player;

    private float distanceCovered;
    public float DistanceCovered => distanceCovered;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distanceCovered = 0f;

        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerRb = player.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Player not found! Make sure PlayerCar is tagged 'Player'");
        }
    }

    void FixedUpdate()
    {
        // 🛑 Stop completely when race finished
        if (RaceManager.Instance != null && RaceManager.Instance.IsRaceFinished)
            return;

        // 🛑 Stop when paused
        if (Time.timeScale == 0f)
            return;

        Move();
        TryPushPlayer();
    }

    public override void Move()
    {
        float delta = forwardSpeed * Time.fixedDeltaTime;

        rb.MovePosition(
            rb.position + transform.forward * delta
        );

        // 📏 Track ONLY AI distance
        distanceCovered += delta;
    }


    void TryPushPlayer()
    {
        if (player == null || playerRb == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pushDistance)
            return;

        Vector3 pushDir = (player.position - transform.position).normalized;
        pushDir.y = 0f;
        pushDir = Vector3.ProjectOnPlane(pushDir, transform.forward);

        playerRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
    }
}
