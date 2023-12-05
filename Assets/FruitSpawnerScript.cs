using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnerScript : MonoBehaviour
{
    public GameObject fruits;
    public float horizontalLimit = 8f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UserControls();
    }

    private void UserControls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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


    public GameObject GetRandomFruit()
    {
        int childrenCount = fruits.transform.childCount;
        if (childrenCount == 0) return null;

        int randomIndex = Random.Range(0, childrenCount);
        return fruits.transform.GetChild(randomIndex).gameObject;
    }


    private void SpawnFruit()
    {
        GameObject fruit = GetRandomFruit();
        Instantiate(fruit, transform.position, transform.rotation);
    }
}