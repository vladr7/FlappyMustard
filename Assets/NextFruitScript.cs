using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFruitScript : MonoBehaviour
{
    public GameObject fruits;
    public GameObject nextFruit;
    public LogicManager logicManager;
    private int lastItem = -1;
    private int secondLastItem = -1;

    void Start()
    {
        nextFruit = GetRandomFruit();
        gameObject.GetComponent<SpriteRenderer>().sprite = nextFruit.GetComponent<SpriteRenderer>().sprite;
    }

    public GameObject GetRandomFruit()
    {
        int index = GetItemBasedOnScore();
        while (index == lastItem && index == secondLastItem)
        {
            index = GetItemBasedOnScore();
        }

        secondLastItem = lastItem;
        lastItem = index;

        return GetFruitForIndex(index);
    }
    
    public GameObject GetFruitForIndex(int index)
    {
        return fruits.transform.GetChild(index).gameObject;
    }

    public void UpdateNextFruit()
    {
        nextFruit = GetRandomFruit();
        gameObject.GetComponent<SpriteRenderer>().sprite = nextFruit.GetComponent<SpriteRenderer>().sprite;
    }
    
    private int GetItemBasedOnScore()
    {
        float[] chances = new float[6]; // Array to hold chances for each item
        float decrement = Mathf.Min(logicManager.score / 100 * 0.01f, 0.33f); // Calculate the decrement

        chances[0] = chances[1] = chances[2] = 0.33f - decrement;
        chances[3] = 0.01f + decrement * 3;
        if (logicManager.score >= 1500) chances[4] = 0.05f;
        if (logicManager.score >= 2000) chances[5] = 0.05f;

        float totalChance = 0f;
        for (int i = 0; i < chances.Length; i++)
        {
            totalChance += chances[i];
        }
        for (int i = 0; i < chances.Length; i++)
        {
            chances[i] /= totalChance;
        }

        float randomPoint = Random.Range(0f, 1f);
        float currentPoint = 0f;
        for (int i = 0; i < chances.Length; i++)
        {
            currentPoint += chances[i];
            if (randomPoint <= currentPoint)
            {
                return i;
            }
        }

        return 0; 
    }
}
