public class Senti : MonoBehaviour
{
    private Animator animator;

    public Transform player;
    public float rotationSpeed = 2.0f;

    private Quaternion originalRotation;
    private bool isInteracting = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (isInteracting)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    public void StartInteraction()
    {
        animator.SetBool("isInteracting", true);
        isInteracting = true;
    }

    public void EndInteraction()
    {
        animator.SetBool("isInteracting", false);
        isInteracting = false;
        transform.rotation = originalRotation;
    }
}
