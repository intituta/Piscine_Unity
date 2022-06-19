using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTilt : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float topLimit = 5f;
    [SerializeField]
    private float bottomLimit = 0.2f;
    private RectTransform rt;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (rt.rotation.eulerAngles.z >= topLimit)
            direction = -1;
        if (rt.rotation.eulerAngles.z <= bottomLimit)
            direction = 1;
        rt.Rotate(new Vector3(0, 0, speed * direction * Time.deltaTime));
    }
}
