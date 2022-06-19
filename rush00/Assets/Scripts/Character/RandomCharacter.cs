using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    [SerializeField] private Sprite[] bodies;
    [SerializeField] private Sprite[] heads;
    
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer head;

    void Awake()
    {
        body.sprite = bodies[Random.Range(0, bodies.Length)];
        head.sprite = heads[Random.Range(0, heads.Length)];

    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
