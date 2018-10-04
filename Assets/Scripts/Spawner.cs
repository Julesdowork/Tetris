using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;

    Next next;

    void Awake()
    {
        next = FindObjectOfType<Next>();
    }

    void Start()
	{
        SpawnNext();
	}

    public void SpawnNext()
    {
        //// Random index
        //int i = Random.Range(0, groups.Length);

        // Spawn group from next object at current location
        Instantiate(groups[next.indexToSpawn], transform);
        next.GetNextGroup();
    }
}