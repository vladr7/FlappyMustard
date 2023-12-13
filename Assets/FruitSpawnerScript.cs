using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    [SerializeField]
    private Transform[] fruitPrefabs; // Array to hold different fruit prefabs
    private Transform[] spawnedFruitTransforms; // Array to hold spawned fruit transforms


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
    }
    
  
    void Update()
    {
        if(!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.T))
        {
            RequestSpawnServerRpc();
        }

        UserControls();
    }
    
    [ServerRpc]
    private void RequestSpawnServerRpc()
    {
        // _spawnedCherryTransform = Instantiate(spawnedCherryPrefab, transform.position, transform.rotation);
        // _spawnedCherryTransform.GetComponent<NetworkObject>().Spawn();
        // Instantiate the selected fruit prefab

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
    public void RequestSpawnAtPositionServerRpc(float x, float y, FruitType fruitType, ServerRpcParams rpcParams = default)
    {
        int index = (int)fruitType; 

        if (index < 0 || index >= fruitPrefabs.Length)
        {
            Debug.LogError("Invalid fruit index.");
            return;
        }

        if (fruitPrefabs[index] == null)
        {
            Debug.LogError("Prefab not found for: " + fruitType.ToString());
            return;
        }

        spawnedFruitTransforms[index] = Instantiate(fruitPrefabs[index], new Vector3(x, y, 0), Quaternion.identity);

        // Play audio (consider playing this on the client side instead for better responsiveness)
        var audioSource = spawnedFruitTransforms[index].GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

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

    private int GetNextFruitIndex()
    {
        // int nextFruitIndex = Random.Range(0, fruitPrefabs.Length);
        int nextFruitIndex = Random.Range(0, 1);
        return nextFruitIndex;
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