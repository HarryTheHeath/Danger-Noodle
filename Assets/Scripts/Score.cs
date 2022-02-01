using UnityEngine;

public class Score : MonoBehaviour
{
    public int PlayerScore { get; private set; } = 0;

    public void AddScore()
    {
        PlayerScore++;
        Debug.Log(PlayerScore);
    }

    public void ResetScore()
    {
        PlayerScore = 0;
    }
}
