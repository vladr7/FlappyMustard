using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFruitScript : MonoBehaviour
{
    public GameObject fruits;
    public GameObject nextFruit;

    void Start()
    {
        nextFruit = GetRandomFruit();
        gameObject.GetComponent<SpriteRenderer>().sprite = nextFruit.GetComponent<SpriteRenderer>().sprite;
    }

    public GameObject GetRandomFruit()
    {
        int childrenCount = fruits.transform.childCount;
        if (childrenCount == 0) return null;

        int randomIndex = Random.Range(0, 4);
        return fruits.transform.GetChild(randomIndex).gameObject;
    }

    public void UpdateNextFruit()
    {
        nextFruit = GetRandomFruit();
        gameObject.GetComponent<SpriteRenderer>().sprite = nextFruit.GetComponent<SpriteRenderer>().sprite;
    }
}
