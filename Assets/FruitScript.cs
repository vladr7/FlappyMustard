using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType : int
{
    Cherry = 1,
    Strawberry = 3,
    Grape = 6,
    Lemon = 10,
    Orange = 15,
    Apple = 21,
    Pear = 28,
    Peach = 36,
    Pineapple = 45,
    Melon = 55,
    Watermelon = 66,
}

public class FruitScript : MonoBehaviour
{
    public LogicManager logicManager;

    // Start is called before the first frame update
    void Start()
    {
        logicManager = GameObject.FindWithTag("LogicManager").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag(FruitType.Cherry.ToString()) &&  other.gameObject.CompareTag(FruitType.Cherry.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Strawberry);
                logicManager.IncreaseScore(1);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Strawberry.ToString()) &&  other.gameObject.CompareTag(FruitType.Strawberry.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Grape);
                logicManager.IncreaseScore(3);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Grape.ToString()) &&  other.gameObject.CompareTag(FruitType.Grape.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Lemon);
                logicManager.IncreaseScore(6);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Lemon.ToString()) &&  other.gameObject.CompareTag(FruitType.Lemon.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Orange);
                logicManager.IncreaseScore(10);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Orange.ToString()) &&  other.gameObject.CompareTag(FruitType.Orange.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Apple);
                logicManager.IncreaseScore(15);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Apple.ToString()) &&  other.gameObject.CompareTag(FruitType.Apple.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Pear);
                logicManager.IncreaseScore(21);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Pear.ToString()) &&  other.gameObject.CompareTag(FruitType.Pear.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Peach);
                logicManager.IncreaseScore(28);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Peach.ToString()) &&  other.gameObject.CompareTag(FruitType.Peach.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Pineapple);
                logicManager.IncreaseScore(36);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Pineapple.ToString()) &&  other.gameObject.CompareTag(FruitType.Pineapple.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y, FruitType.Melon);
                logicManager.IncreaseScore(45);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Melon.ToString()) &&  other.gameObject.CompareTag(FruitType.Melon.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                SpawnFruit(transform.position.x, transform.position.y,FruitType.Watermelon);
                logicManager.IncreaseScore(55);
            }
        }
        
        if (gameObject.CompareTag(FruitType.Watermelon.ToString()) &&  other.gameObject.CompareTag(FruitType.Watermelon.ToString()))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            logicManager.IncreaseScore(66);
        }
    }
    
    public void SpawnFruit(float x, float y, FruitType fruitType)
    {
        string path = "Fruits/" + fruitType.ToString(); 
        GameObject fruitPrefab = Resources.Load<GameObject>(path);
        if (fruitPrefab != null)
        {
            Instantiate(fruitPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab not found for: " + fruitType.ToString());
        }

        
    }
    
    
}
