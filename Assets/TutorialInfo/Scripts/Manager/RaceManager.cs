using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    public float raceDistance = 200f;
    public bool IsRaceFinished { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ResetRace()
    {
        IsRaceFinished = false;
    }

    public void FinishRace()
    {
        IsRaceFinished = true;
    }
}
