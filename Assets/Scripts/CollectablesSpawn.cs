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

    public IEnumerator SpawnNow() 
    {
        while(true) 
        {
            if (GameManager.IsGameOver)
            {
                yield break; // Stop the coroutine if the game is over
            }

            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Instantiate(
                prefabs[Random.Range(0, prefabs.Count)],
                new Vector3(Random.Range(-7.06f, 7.68f), Random.Range(-3.96f, 3.49f), 0),
                Quaternion.identity
            );
        }
    }

}
