using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldSpawner : MonoBehaviour
{
    public List<GameObject> bfPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bfPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
