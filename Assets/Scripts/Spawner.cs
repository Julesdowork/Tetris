using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;

	void Start()
	{
        // Spawn initial group
        SpawnNext();
	}

    public void SpawnNext()
    {
        // Random index
        int i = Random.Range(0, groups.Length);

        // Spawn group at current location
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}