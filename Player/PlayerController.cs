using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    CharacterController player;
    PlayerHealth playerHealth;
    [SerializeField] private float moveSpeed = 3.0f;

    private void Awake() {
        player = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerHealth.IsDead())
            return;

        PlayerInput();

    }

    private void PlayerInput()
    {
        player.Move(player.transform.forward * moveSpeed * Time.deltaTime); //always move in a direction

        float leftRight = Input.GetAxisRaw("Horizontal"); //-1 for A, 1 for D
        float upDown = Input.GetAxisRaw("Vertical");//-1 for W, 1 for S

        Vector3 direction = new Vector3(leftRight, 0f, upDown).normalized;

        if (direction.magnitude > float.Epsilon)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}
