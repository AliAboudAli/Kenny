using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float crouchSpeed;
    
    private float maxJump = 6f;
    public GameObject player;
    public bool isGrounded;
    public float walkSpeed = 3f;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * walkSpeed;
        float vertical = Input.GetAxis("Vertical")* walkSpeed;
        
        //Vector has to be zero at start 
        Vector3 move = (transform.right * horizontal * vertical) * Time.deltaTime;
        
        move += horizontal * transform.right;
        move += vertical * transform.forward;
        
        transform.Translate(move, Space.World);

        Jump();
        Crouch();
        Sprint();
    }

    public void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void Crouch()
    {
        if(Input.GetKey(KeyCode.C) && isGrounded == false)
        {
            crouchSpeed = 0.50f;
            Debug.Log("Crouch is true" + crouchSpeed);
            isGrounded = true;
        }
    }

    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = 5f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
}
