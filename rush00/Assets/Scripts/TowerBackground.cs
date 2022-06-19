using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBackground : MonoBehaviour
{
    private RectTransform rt;
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private float topLimit = 1.8f;
    [SerializeField]
    private float bottomLimit = 0.24f;

    void Start()
    {
        rt = GetComponent<RectTransform>();    
    }

    // Update is called once per frame
    void Update()
    {
        
        rt.localScale += new Vector3(rt.localScale.x * speed * Time.deltaTime, rt.localScale.y * speed * Time.deltaTime, 0);
        if (rt.localScale.y >= topLimit)
            rt.localScale = new Vector3(bottomLimit, bottomLimit, 0);
    }
}
