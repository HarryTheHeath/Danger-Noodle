using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private Food foodPrefab;
    public Food avokado;
    public Food avokado2;
    public Food sushi;
    public Food sushi2;
    private int lastRandom;

    Vector3 GetFoodSpawnPosition()
    {
        return new Vector3(
            Random.Range(-3, 4),
            Random.Range(-3, 4)
        );
    }

    public void SpawnFood()
    {
        var random = Random.Range(0, 3);

        if (random == lastRandom)
        {
            NoRepeats(ref random);
        }
        switch (random)
        {
            case 0:
                foodPrefab = avokado;
                //Debug.Log("Spawn Avocado");
                lastRandom = 0;
                break;
            case 1:
                foodPrefab = avokado2;
                //Debug.Log("Spawn Happy Avocado");
                lastRandom = 1;
                break;
            case 2:
                foodPrefab = sushi;
                //Debug.Log("Spawn Salmon Sushi");
                lastRandom = 2;
                break;
            case 3:
                foodPrefab = sushi2;
                //Debug.Log("Spawn Avocado Sushi");
                lastRandom = 3;
                break;
        }
        Instantiate(foodPrefab, GetFoodSpawnPosition(), Quaternion.identity);
    }

    public void NoRepeats(ref int random)
    {
        while (random == lastRandom)
        {
            var random2 = Random.Range(1, 4);
            random = (4 - random2);
            if (random != lastRandom)
            {
                break;
            }
        }
        //Debug.Log($"Random changed from {lastRandom} to {random}");
    }
}
