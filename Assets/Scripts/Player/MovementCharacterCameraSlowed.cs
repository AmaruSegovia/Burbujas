using UnityEngine;

public class MovementCharacterCameraSlowed : MonoBehaviour
{
    public float speed = 5f;
    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new(moveX, moveY, 0);
        transform.Translate(speed * Time.unscaledDeltaTime * move);
    }
}