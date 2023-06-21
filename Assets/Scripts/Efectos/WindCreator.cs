using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCreator : MonoBehaviour
{
    public GameObject[] winds;
    public float minCD;
    public float maxtCD;
    public float numThreads;
    public float maxRadiusDistance;
    public float maxHeigthDistance;

    public Transform origin;

    private void Start()
    {
        for(int i = 0; i < numThreads; i++)
        {
            StartCoroutine(SpawnWind());
        }
    }

    IEnumerator SpawnWind()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minCD, maxtCD));
            Vector3 pos = origin.position + new Vector3(Random.Range(-maxRadiusDistance, maxRadiusDistance), Random.Range(0, maxHeigthDistance), Random.Range(-maxRadiusDistance, maxRadiusDistance));
            int selection = Random.Range(0, winds.Length);
            GameObject wind = Instantiate(winds[selection]);
            print("selection " + selection);
            wind.transform.position = pos;
            wind.GetComponent<ParticleSystem>().Play();
            Destroy(wind,30);
        }
    }
}
