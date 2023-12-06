using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class FruitSpawnerScript : MonoBehaviour
{
    public float horizontalLimit = 8f;
    public NextFruitScript nextFruitScript;
    private GameObject _currentFruit;
    public GameObject currentFruitDrop;

    void Start()
    {
        nextFruitScript = GameObject.FindWithTag("NextFruit").GetComponent<NextFruitScript>();
        _currentFruit = nextFruitScript.GetRandomFruit();
        UpdateFruitDropUi();
    }

    void Update()
    {
        UserControls();
    }

    private void UserControls()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            SpawnFruit();
        }
        
        PlayerMouseControl();
    }

    private void PlayerMouseControl()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Camera.main.transform.position.y));
        float clampedX = Mathf.Clamp(mousePosition.x, -horizontalLimit, horizontalLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void SpawnFruit()
    {
        Instantiate(_currentFruit, transform.position, transform.rotation);
        _currentFruit = nextFruitScript.nextFruit;
        UpdateFruitDropUi();
        nextFruitScript.UpdateNextFruit();
    }

    private void UpdateFruitDropUi()
    {
        currentFruitDrop.transform.localScale = _currentFruit.transform.localScale * 0.6f;
        currentFruitDrop.GetComponent<SpriteRenderer>().sprite = _currentFruit.GetComponent<SpriteRenderer>().sprite;
    }
}