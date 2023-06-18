using UnityEngine;

public class StopCoin : MonoBehaviour
{
    public float stopY = 0.5f;
    private Rigidbody rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= stopY)
        {
            rb.isKinematic = true;
           // animator.SetBool("landed", true);
        }
    }
}
