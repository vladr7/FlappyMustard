using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StrawberryScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnStrawberry(float x, float y)
    {
        Instantiate(gameObject, new Vector3(x, y, 0), Quaternion.identity);
    }
}
