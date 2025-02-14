using UnityEngine;

public class RotacionPunio : MonoBehaviour
{
    Camera mainCamera;
    Vector3 mousePosition;
    [SerializeField] SpriteRenderer spriteRenderer;
    GameObject player;
    PlayerPunch playerPunch;
    private float rotZ;
    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPunch = player.GetComponent<PlayerPunch>();
    }
    private void Update()
    {
        transform.position = player.transform.position;
        RotationPuch();
    }
    private void RotationPuch()
    {
        if (playerPunch.isPunch)
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 rotation = mousePosition - transform.position;
            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        spriteRenderer.flipY = rotZ > 90 || rotZ < -90;
    }
}