using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject coin1;
    public GameObject coin2;
    public GameObject coin5;
    private int sum;

    void IncreaseSum(int n)
    {
        sum += n;
        Debug.Log("sum increased by " + n + " to " + sum);
    }
    void DecreaseSum(int n)
    {
        sum -= n;
        Debug.Log("sum decreased by " + n + " to " + sum);
    }

    private bool CoinsColiding(Vector3 vector, GameObject[] coins)
    {
        //function that checks if there are any coins coliding before spawning a new one

        float deltaX, deltaY;
        foreach (GameObject coin in coins)
        {

            Debug.Log("Checking for collision with " + coin.name);

            deltaX = coin.transform.position.x-vector.x;
            deltaY = coin.transform.position.y-vector.y;
            if ((deltaX > -1f && deltaX < 1f) || (deltaY > -1f && deltaY < 1f))
                return true;
        }
        return false;
    }
    private Vector3 ChooseCoords()
    {
        //function that chooses coordinates for a new coin (and checks if there are any coins coliding)

        float z = 0;
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-4f, 4f);
        Vector3 coords = new Vector3(x, y, z);

        //this part below is supposed to ensure that no coins are overlaping. It makes unity crash.
        //The problem is either here or in the CoinsColiding function itself.

        /*GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        if (coins.Length > 0)
        {
            while (CoinsColiding(coords, coins))
            {
                x = Random.Range(-5f, 5f);
                y = Random.Range(-4f, 4f);
                coords.Set(x, y, z);
            }
        }*/

        return coords;
    }
    private void InstantiateCoins(int n, GameObject coin)
    {
        //spawns n coins of chosen variant

        for (int i = 0; i < n; i++)
        {
            Instantiate(coin, ChooseCoords(), Quaternion.identity);
        }
    }
    public void CheckWin()
    {
        if (sum==20)
        {
            Debug.Log("Task complete successfully");
            // play the next sequence
        }
        else
        {
            Debug.Log("Task failed, the sum was " + sum.ToString());
            // replay this sequence
        }

    }

    void Start()
    {
        InstantiateCoins(4, coin1);
        InstantiateCoins(4, coin2);
        InstantiateCoins(8, coin5);
    }

}
