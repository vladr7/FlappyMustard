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
    private Transform spawnedObjectPrefab;
    private Transform spawnedObjectTransform;

    public override void OnNetworkSpawn()
    {
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
    private void RequestSpawnServerRpc(ServerRpcParams rpcParams = default)
    {
        Transform spawnedObjectTransform = Instantiate(spawnedObjectPrefab, transform.position, transform.rotation);
        spawnedObjectTransform.GetComponent<NetworkObject>().Spawn();
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