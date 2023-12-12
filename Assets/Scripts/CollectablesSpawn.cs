using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesSpawn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs;

    public float minTime = 1.0f;

    public float maxTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {

        StartCoroutine(SpawnNow());
    }

    private void OnDisable() {

        StopCoroutine(SpawnNow());
    }

    public IEnumerator SpawnNow() {

        while(true) {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Instantiate(
                prefabs[Random.Range(0, prefabs.Count)],
                new Vector3(Random.Range(-75, 7.5f), Random.Range(0.5f, 36.1f), 0),
                Quaternion.identity
            );
        }
    }
}
