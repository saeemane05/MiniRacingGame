using UnityEngine;

public class FinishChecker : MonoBehaviour
{
    public PlayerCar player;
    public AICar ai;
    public UIManager uiManager;

    private bool raceEnded = false;

    void Update()
    {
        if (raceEnded) return;

        if (player.DistanceCovered >= RaceManager.Instance.raceDistance ||
            ai.DistanceCovered >= RaceManager.Instance.raceDistance)
        {
            raceEnded = true;

            // 🔒 LOCK THE RACE GLOBALLY
            RaceManager.Instance.FinishRace();

            string winnerName =
                player.DistanceCovered >= ai.DistanceCovered
                ? "PLAYER WINS!"
                : "AI WINS!";

            uiManager.ShowWinner(winnerName);

            Debug.Log("SHOW WINNER CALLED");
        }
    }
}
