using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnerScript : MonoBehaviour
{
    public GameObject fruit;

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
    }

    private void SpawnFruit()
    {
        Instantiate(fruit, transform.position, transform.rotation);
    }
}
