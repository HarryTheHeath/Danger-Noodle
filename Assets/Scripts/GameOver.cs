using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Score score;
    public float respawnTime = 3f;

    private void Awake()
    {
        score = GetComponent<Score>();
    }

    public void TriggerReset()
    {
        StartCoroutine(Reset());
    }
    
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(respawnTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        score.ResetScore();
    }
}
