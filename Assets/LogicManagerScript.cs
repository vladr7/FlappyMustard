using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public GameObject fruits;
    public GameObject currentFruit;
    public GameObject nextFruit;

    // Start is called before the first frame update
    void Start()
    {
        currentFruit = GetRandomFruit();
        nextFruit = GetRandomFruit();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject GetRandomFruit()
    {
        int childrenCount = fruits.transform.childCount;
        if (childrenCount == 0) return null;

        int randomIndex = Random.Range(0, childrenCount);
        return fruits.transform.GetChild(randomIndex).gameObject;
    }
    
    public void UpdateCurrentAndNextFruit()
    {
        currentFruit = nextFruit;
        nextFruit = GetRandomFruit();
    }
}