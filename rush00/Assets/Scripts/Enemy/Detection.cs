using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField]
    private EnemyPatrol enemyScript;

    [SerializeField] LayerMask layers;

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "PlayerSound")     
        {
            RaycastHit2D hit;
            Vector2 fromPosition = transform.position;
            Vector2 toPosition = collision.transform.position;
            Vector2 direction = toPosition - fromPosition;


            hit = Physics2D.Raycast(fromPosition, direction, layers);
            Debug.DrawRay(fromPosition, direction * 10f, Color.red);
            
            if (hit.collider.tag == "Player" || hit.collider.tag == "PlayerSound")
            {
                enemyScript.isDetectedPlayer = true;
            }
            // Vector2 direction = new Vector2(
            //     enemyScript.gameObject.transform.position.x - collision.transform.position.x,
            //     enemyScript.gameObject.transform.position.y - collision.transform.position.y
            // );
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            enemyScript.isDetectedPlayer = false;
    }
}
