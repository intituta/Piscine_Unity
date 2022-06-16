using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Spawner : MonoBehaviour
{
    public float maxTime = 1;
    private float timer = 0;
    public GameObject Pipes;
    public float distance;

    void Start()
    {
        GameObject newpipe = Instantiate(Pipes);
        newpipe.transform.position = transform.position + new Vector3(0, Random.Range(-distance, distance), 0);
    }

    void Update()
    {
        if (timer > maxTime)
        {
            GameObject newpipe = Instantiate(Pipes);
			newpipe.transform.position = transform.position + new Vector3(0, Random.Range(-distance, distance), 0);
			Destroy(newpipe, 15);
			timer = 0;
        }
		timer += Time.deltaTime;
    }
}
