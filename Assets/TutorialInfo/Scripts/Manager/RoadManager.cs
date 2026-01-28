using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public Transform roadParent;   // Empty "Road" object
    public Transform playerCar;
    public float roadLength = 100f;

    private Transform[] roadPieces;
    private float lastZ;

    void Start()
    {
        if (roadParent == null)
        {
            Debug.LogError("Road Parent is NOT assigned!");
            return;
        }

        int count = roadParent.childCount;

        if (count == 0)
        {
            Debug.LogError("Road Parent has NO child road pieces!");
            return;
        }

        roadPieces = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            roadPieces[i] = roadParent.GetChild(i);
        }

        lastZ = roadPieces[roadPieces.Length - 1].position.z;
    }

    void Update()
    {
        if (RaceManager.Instance != null && RaceManager.Instance.IsRaceFinished)
            return;


        foreach (Transform road in roadPieces)
        {
            if (road.position.z < playerCar.position.z - roadLength)
            {
                road.position = new Vector3(
                    road.position.x,
                    road.position.y,
                    lastZ + roadLength
                );

                lastZ += roadLength;
            }
        }
    }
}
