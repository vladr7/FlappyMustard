using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Netcode;
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

public class FruitScript : NetworkBehaviour
{
    public LogicManager logicManager;

    // public AudioSource audioSource;
    // public GameObject whiteSplash;
    public FruitSpawnerScript fruitSpawnerScript;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        fruitSpawnerScript = GameObject.FindWithTag("FruitSpawner").GetComponent<FruitSpawnerScript>();
        logicManager = GameObject.FindWithTag("LogicManager").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag(FruitType.Cherry.ToString()) &&
            other.gameObject.CompareTag(FruitType.Cherry.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Strawberry);
                logicManager.IncreaseScoreServerRpc(1);
            }
        }

        if (gameObject.CompareTag(FruitType.Strawberry.ToString()) &&
            other.gameObject.CompareTag(FruitType.Strawberry.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Grape);
                logicManager.IncreaseScoreServerRpc(3);
            }
        }

        if (gameObject.CompareTag(FruitType.Grape.ToString()) &&
            other.gameObject.CompareTag(FruitType.Grape.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Lemon);
                logicManager.IncreaseScoreServerRpc(6);
            }
        }

        if (gameObject.CompareTag(FruitType.Lemon.ToString()) &&
            other.gameObject.CompareTag(FruitType.Lemon.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Orange);
                logicManager.IncreaseScoreServerRpc(10);
            }
        }

        if (gameObject.CompareTag(FruitType.Orange.ToString()) &&
            other.gameObject.CompareTag(FruitType.Orange.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Apple);
                logicManager.IncreaseScoreServerRpc(15);
            }
        }

        if (gameObject.CompareTag(FruitType.Apple.ToString()) &&
            other.gameObject.CompareTag(FruitType.Apple.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Pear);
                logicManager.IncreaseScoreServerRpc(21);
            }
        }

        if (gameObject.CompareTag(FruitType.Pear.ToString()) && other.gameObject.CompareTag(FruitType.Pear.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Peach);
                logicManager.IncreaseScoreServerRpc(28);
            }
        }

        if (gameObject.CompareTag(FruitType.Peach.ToString()) &&
            other.gameObject.CompareTag(FruitType.Peach.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Pineapple);
                logicManager.IncreaseScoreServerRpc(36);
            }
        }

        if (gameObject.CompareTag(FruitType.Pineapple.ToString()) &&
            other.gameObject.CompareTag(FruitType.Pineapple.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Melon);
                logicManager.IncreaseScoreServerRpc(45);
            }
        }

        if (gameObject.CompareTag(FruitType.Melon.ToString()) &&
            other.gameObject.CompareTag(FruitType.Melon.ToString()))
        {
            DestroyCollidedFruits(other);

            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                fruitSpawnerScript.RequestSpawnAtPositionServerRpc(transform.position.x, transform.position.y,
                    FruitType.Watermelon);
                logicManager.IncreaseScoreServerRpc(55);
            }
        }

        if (gameObject.CompareTag(FruitType.Watermelon.ToString()) &&
            other.gameObject.CompareTag(FruitType.Watermelon.ToString()))
        {
            DestroyCollidedFruits(other);

            logicManager.IncreaseScoreServerRpc(66);
        }
    }

    private void DestroyCollidedFruits(Collision2D other)
    {
        Destroy(other.gameObject);
        SpawnWhiteSplash(other.gameObject);
        Destroy(gameObject);
        SpawnWhiteSplash(gameObject);
    }

    private void SpawnWhiteSplash(GameObject gameObject)
    {
        // GameObject splashInstance = Instantiate(whiteSplash,
        //     new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0),
        //     Quaternion.identity);
        // Destroy(splashInstance, 0.03f);
    }


    public void SpawnFruit(float x, float y, FruitType fruitType)
    {
        string path = "Fruits/" + fruitType.ToString();
        GameObject fruitPrefab = Resources.Load<GameObject>(path);
        if (fruitPrefab != null)
        {
            GameObject newFruit = Instantiate(fruitPrefab, new Vector3(x, y, 0), Quaternion.identity);
            newFruit.GetComponent<AudioSource>().Play();
        }
        else
        {
            Debug.LogError("Prefab not found for: " + fruitType.ToString());
        }
    }
}