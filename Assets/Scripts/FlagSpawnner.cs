using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawnner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] flags;

    private GameObject spawnnedFlag;

    private int randomIndex;

    private void Start()
    {
        Invoke("SpawnFlag", 5f);
    }

    public void SpawnFlag()
    {
        randomIndex = Random.Range(0, flags.Length);

        spawnnedFlag = Instantiate(flags[randomIndex]);

        spawnnedFlag.transform.position = transform.position;
    }
}
