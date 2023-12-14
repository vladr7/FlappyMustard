using System;
using Unity.Netcode;
using UnityEngine;

public class FruitSpawnerScript : NetworkBehaviour
{
    // public float horizontalLimit = 10.5f;
    public NextFruitScript nextFruitScript;
    // private Transform _currentFruit;
    // public float spawnRate = 1f;
    // private float _lastSpawnTime;
    // private bool _firstSpawn = true;
    // public LogicManager logicManager;

    public Transform currentFruitDropTransform;
    
    [SerializeField] private Transform[] fruitPrefabs; // Array to hold different fruit prefabs
    private Transform[] spawnedFruitTransforms; // Array to hold spawned fruit transforms
    
    private NetworkVariable<int> currentFruitIndex = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        spawnedFruitTransforms = new Transform[fruitPrefabs.Length];
        transform.position = new Vector3(transform.position.x, 14.4f, transform.position.z);
        // _lastSpawnTime = spawnRate;
        nextFruitScript = GameObject.FindWithTag("NextFruit").GetComponent<NextFruitScript>();
        // logicManager = GameObject.FindWithTag("LogicManager").GetComponent<LogicManager>();
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
    
    private void OnEnable()
    {
        currentFruitIndex.OnValueChanged += OnCurrentFruitIndexChanged;
    }

    private void OnDisable()
    {
        currentFruitIndex.OnValueChanged -= OnCurrentFruitIndexChanged;
    }

    private void OnCurrentFruitIndexChanged(int oldIndex, int newIndex)
    {
        UpdateCurrentFruitDropSprite(fruitPrefabs[newIndex]);
    }


    [ServerRpc]
    private void RequestSpawnServerRpc()
    {
        HandleFruitSpawn();
    }
    
    private void HandleFruitSpawn()
    {
        spawnedFruitTransforms[currentFruitIndex.Value] = Instantiate(fruitPrefabs[currentFruitIndex.Value], transform.position, transform.rotation);

        var networkObject = spawnedFruitTransforms[currentFruitIndex.Value].GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.Spawn();
            currentFruitIndex.Value = GetNextFruitIndex();
        }
        else
        {
            Debug.LogError("Spawned fruit does not have a NetworkObject component.");
        }
    }
    
    private void UpdateCurrentFruitDropSprite(Transform newFruitTransform)
    {
        Debug.Log("UpdateCurrentFruitDropSprite");
        if (currentFruitDropTransform != null)
        {
            Debug.Log("currentFruitDropTransform != null");
            SpriteRenderer newFruitSpriteRenderer = newFruitTransform.GetComponent<SpriteRenderer>();
            SpriteRenderer currentFruitDropSpriteRenderer = currentFruitDropTransform.GetComponent<SpriteRenderer>();

            if (newFruitSpriteRenderer != null && currentFruitDropSpriteRenderer != null)
            {
                Debug.Log("newFruitSpriteRenderer != null && currentFruitDropSpriteRenderer != null");
                currentFruitDropSpriteRenderer.sprite = newFruitSpriteRenderer.sprite;
            }
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
    
 
    
    private int GetNextFruitIndex()
    {
        int nextFruitIndex = nextFruitScript.GetRandomFruitIndex();
        UpdateCurrentFruitDropSprite(fruitPrefabs[nextFruitIndex]);
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
}