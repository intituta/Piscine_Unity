using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    private Rigidbody2D m_rb2d;
    private Vector3 m_Velocity = Vector3.zero;

    public float runSpeed = 40f;
    public Animator animator;
    public Vector2 direction;

    float moveX = 0f;
    float moveY = 0f;
    [SerializeField]
    private Vector3 stairPos;
    [SerializeField]
    private string levelName;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        if (PlayerPrefs.GetInt(levelName) == 1)
            transform.position = stairPos;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal") * runSpeed;
        moveY = Input.GetAxisRaw("Vertical") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(moveX + moveY));
    }

    void FixedUpdate()
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(moveX * 5f, moveY * 5f);
        // And then smoothing it out and applying it to the character
        m_rb2d.velocity = Vector3.SmoothDamp(m_rb2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        faceMouse();

    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = new Vector2(
            transform.position.x - mousePosition.x,
            transform.position.y - mousePosition.y
        );

        transform.up = direction;
    }
}
