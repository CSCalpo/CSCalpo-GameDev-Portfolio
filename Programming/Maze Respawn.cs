public class RespawnScript : MonoBehaviour
{
    public GameObject Respawn;

    private void OnTriggerEnter(Collider collider)
    {
        CharacterController controller = collider.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            collider.transform.position = Respawn.transform.position;
            controller.enabled = true;
        }

        Debug.Log("Whoops");

        if (collider.CompareTag("Player"))
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.MovePosition(Respawn.transform.position);
            }
            else
            {
                collider.transform.position = Respawn.transform.position;
            }
        }
    }
}
