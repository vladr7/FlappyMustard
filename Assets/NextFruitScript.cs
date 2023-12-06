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
        gameObject.transform.localScale = nextFruit.transform.localScale;
    }
    
    private int GetItemBasedOnScore()
    {
        float[] chances = new float[8]; // Array to hold chances for each item, up to item 7

        // Basic chances setup
        float decrement = Mathf.Min(logicManager.score / 100 * 0.01f, 0.33f);
        chances[0] = chances[1] = chances[2] = 0.33f - decrement;
        chances[3] = 0.01f + decrement * 3;

        // Adjust chances based on score thresholds
        if (logicManager.score > 100) chances[4] += 0.10f;
        if (logicManager.score > 300) chances[5] += 0.10f;
        if (logicManager.score > 500) chances[6] += 0.10f;
        if (logicManager.score > 800) chances[7] += 0.10f;

        // Normalize chances
        float totalChance = 0f;
        for (int i = 0; i < chances.Length; i++)
        {
            totalChance += chances[i];
        }
        for (int i = 0; i < chances.Length; i++)
        {
            chances[i] /= totalChance;
        }

        // Generate a random item, ensuring it's not the same as the last
        int newItem;
        do
        {
            newItem = GenerateRandomItem(chances);
        } while (newItem == lastItem);

        lastItem = newItem; // Update the last generated item
        return newItem;
    }

    private int GenerateRandomItem(float[] chances)
    {
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
        return 0; // Default return
    }
}
