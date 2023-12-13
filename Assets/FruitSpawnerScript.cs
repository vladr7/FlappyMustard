using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FruitSpawnerScript : NetworkBehaviour
{
    // public float horizontalLimit = 10.5f;
    // public NextFruitScript nextFruitScript;
    // private Transform _currentFruit;
    // public GameObject currentFruitDrop;
    // public float spawnRate = 1f;
    // private float _lastSpawnTime;
    // private bool _firstSpawn = true;
    // public LogicManager logicManager;

    [SerializeField] private Transform[] fruitPrefabs; // Array to hold different fruit prefabs
    private Transform[] spawnedFruitTransforms; // Array to hold spawned fruit transforms
    
    private NetworkVariable<int> score = new NetworkVariable<int>(
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    public override void OnNetworkSpawn()
    {
        spawnedFruitTransforms = new Transform[fruitPrefabs.Length];
        transform.position = new Vector3(transform.position.x, 14.4f, transform.position.z);
        // _lastSpawnTime = spawnRate;
        // nextFruitScript = GameObject.FindWithTag("NextFruit").GetComponent<NextFruitScript>();
        // logicManager = GameObject.FindWithTag("LogicManager").GetComponent<LogicManager>();
        // _currentFruit = nextFruitScript.GetRandomFruit().transform;
        // _currentFruit.gameObject.SetActive(true);
        // UpdateFruitDropUi();
        
        score.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + " ; score: " + score.Value);
        };
    }

    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.T))
        {
            RequestSpawnServerRpc();
        }

        UserControls();
    }

    [ServerRpc]
    private void RequestSpawnServerRpc()
    {

        var index = GetNextFruitIndex();
        spawnedFruitTransforms[index] = Instantiate(fruitPrefabs[index], transform.position, transform.rotation);

        var networkObject = spawnedFruitTransforms[index].GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.Spawn();
        }
        else
        {
            Debug.LogError("Spawned fruit does not have a NetworkObject component.");
        }
    }

    [ServerRpc]
    public void RequestSpawnAtPositionServerRpc(float x, float y, FruitType fruitType,
        ServerRpcParams rpcParams = default)
    {
        int index = GetIndexFromFruitType(fruitType);

        if (index < 0 || index >= fruitPrefabs.Length)
        {
            Debug.LogError("Invalid fruit index " + index + " for fruit type " + fruitType.ToString());
            return;
        }


        if (fruitPrefabs[index] == null)
        {
            Debug.LogError("Prefab not found for: " + fruitType.ToString());
            return;
        }

        spawnedFruitTransforms[index] = Instantiate(fruitPrefabs[index], new Vector3(x, y, 0), Quaternion.identity);

        var networkObject = spawnedFruitTransforms[index].GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.Spawn();
        }
        else
        {
            Debug.LogError("Spawned fruit does not have a NetworkObject component.");
        }
    }
    
    [ServerRpc]
    public void IncreaseScoreServerRpc(int amount)
    {
        if (!IsOwner)
        {
            Debug.LogError("Only the owner can increase the score.");
            return;
        }
        score.Value += amount;
        Debug.Log("Current score: " + score.Value);
    }
    
    private int GetNextFruitIndex()
    {
        // int nextFruitIndex = Random.Range(0, fruitPrefabs.Length);
        int nextFruitIndex = Random.Range(0, 1);
        return nextFruitIndex;
    }

    private int GetIndexFromFruitType(FruitType fruitType)
    {
        switch (fruitType)
        {
            case FruitType.Cherry:
                return 0;
            case FruitType.Strawberry:
                return 1;
            case FruitType.Grape:
                return 2;
            case FruitType.Lemon:
                return 3;
            case FruitType.Orange:
                return 4;
            case FruitType.Apple:
                return 5;
            case FruitType.Pear:
                return 6;
            case FruitType.Peach:
                return 7;
            case FruitType.Pineapple:
                return 8;
            case FruitType.Melon:
                return 9;
            case FruitType.Watermelon:
                return 10;
            default:
                throw new ArgumentOutOfRangeException(nameof(fruitType), fruitType, null);
        }
    }
    
    private void UserControls()
    {
        // if(logicManager.isPaused || logicManager.gameHasEnded) return;
        // if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && ((Time.time - _lastSpawnTime >= spawnRate) || _firstSpawn))
        // {
        // _firstSpawn = false;
        // _lastSpawnTime = Time.time;
        // currentFruitDrop.SetActive(false);
        // Invoke(nameof(ManageCurrentAndNextFruitAfterSpawning), 1f);
        // }

        PlayerMouseControl();
    }

    private void PlayerMouseControl()
    {
        float localHorizontalLimit;
        // if(_currentFruit.CompareTag(FruitType.Cherry.ToString()) || _currentFruit.CompareTag(FruitType.Strawberry.ToString()))
        //     localHorizontalLimit = horizontalLimit;
        // else
        //     localHorizontalLimit = horizontalLimit - _currentFruit.transform.localScale.x;
        localHorizontalLimit = 10.5f;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Camera.main.transform.position.y));

        float clampedX = Mathf.Clamp(mousePosition.x, -localHorizontalLimit, localHorizontalLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }


    private void ManageCurrentAndNextFruitAfterSpawning()
    {
        // _currentFruit = nextFruitScript.nextFruit.transform;
        // _currentFruit.gameObject.SetActive(true);
        // UpdateFruitDropUi();
        // nextFruitScript.UpdateNextFruit();
        // currentFruitDrop.SetActive(true);
    }

    // private void UpdateFruitDropUi()
    // {
    //         currentFruitDrop.transform.localScale = _currentFruit.transform.localScale * 0.6f;
    //         currentFruitDrop.GetComponent<SpriteRenderer>().sprite = _currentFruit.GetComponent<SpriteRenderer>().sprite;
    // }
}