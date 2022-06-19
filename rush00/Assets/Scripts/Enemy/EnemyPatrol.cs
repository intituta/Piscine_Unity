using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPatrol : MonoBehaviour
{
    private enum State {
        Roaming,
        ChaseTarget,
        BackToStart,
    }
    private State state;
    
    public Transform playerTarget;
    [SerializeField] private EnemyWeapon enemyWeapon;
    public bool isDetectedPlayer = false;
    
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int startingPoint;
    [SerializeField] private float speed = 2;
    [SerializeField] private float viewRange = 8f;

    // for pathfinding
    [SerializeField] public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    //bool reachedEndOfPath = false;
    // public Transform target;

    Seeker seeker;
    Rigidbody2D rb;


    public Animator animator;

    private int i;
    private Coroutine updatePathRoutine;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        rb.position = patrolPoints[startingPoint].position;
        state = State.Roaming;

        // InvokeRepeating("UpdatePath", 0f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("CURRENT STATE:" + state);
        switch (state) {
            default:
            case State.Roaming:
                Patrol();
                break;

            case State.ChaseTarget:
                if (updatePathRoutine == null)
                    updatePathRoutine = StartCoroutine(UpdatePath(playerTarget));
                MoveTo(speed * 2);
                enemyWeapon.ShootPlayer(transform.up);
                
                // Stop chasing if too far                
                if (playerTarget != null && Vector2.Distance(rb.position, playerTarget.position) > viewRange)
                {
                    isDetectedPlayer = false;
                    StopCoroutine(updatePathRoutine);
                    updatePathRoutine = null;
                    state = State.BackToStart;
                }
                break;

            case State.BackToStart:
                if (updatePathRoutine == null)
                    updatePathRoutine = StartCoroutine(UpdatePath(patrolPoints[i]));
                StopCoroutine(updatePathRoutine);
                MoveTo(speed);

                if (Vector2.Distance(rb.position, patrolPoints[i].position) < 0.5f)
                {
                    updatePathRoutine = null;
                    state = State.Roaming;
                }

                if (isDetectedPlayer == true)
                {
                    updatePathRoutine = null;
                    state = State.ChaseTarget;
                }
                break;

        }
    }

    void Patrol()
    {
        if (isDetectedPlayer == true)
            state = State.ChaseTarget;

        if (patrolPoints != null)
        {
            // Update to next patrol point
            if (Vector2.Distance(rb.position, patrolPoints[i].position) < 0.5f)
            {
                i++;
                if (i == patrolPoints.Length)
                    i = 0;
                if (updatePathRoutine == null)
                    updatePathRoutine = StartCoroutine(UpdatePath(patrolPoints[i]));
                StopCoroutine(updatePathRoutine);
                updatePathRoutine = null;
            }

            MoveTo(speed);
            // Vector2 direction = new Vector2(
            //     transform.position.x - patrolPoints[i].position.x,
            //     transform.position.y - patrolPoints[i].position.y
            // );
            // transform.up = direction;

            // animator.SetFloat("Speed", 1);

            // transform.position = Vector2.MoveTowards(transform.position, patrolPoints[i].position, speed * Time.deltaTime);
            // if (transform.position == patrolPoints[i].transform.position)
            // {
            //     patrolPoints[i] = patrolPoints.GetComponent<CheckPoint>().patrolPoints;
            // }
        }

    }

    void MoveTo(float m_speed)
    {
        if (path == null)
            return;
        
        if (currentWaypoint >= path.vectorPath.Count)
        {
            //reachedEndOfPath = true;
            return;
        }
        else {
            //reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if (isDetectedPlayer == true)
            transform.up = ((Vector2)playerTarget.position - rb.position).normalized * -1;
        else
            transform.up = direction * -1;

        Vector2 force = direction * m_speed * Time.deltaTime;
        rb.AddForce(force);
        // transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;
        
    }

    IEnumerator UpdatePath(Transform target)
    {
        while(true)
        {
            if (seeker.IsDone())
            {
                Transform targetNew = target;
                seeker.StartPath(rb.position, targetNew.position, OnPathComplete);
                yield return new WaitForSeconds(1f);
            }
        }
            
    }

    
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
