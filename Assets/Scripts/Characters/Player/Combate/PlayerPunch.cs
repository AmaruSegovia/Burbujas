using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public GameObject fist; // Referencia al objeto del puño
    public Animator fistAnimator; // Referencia al Animator del puño
    public KeyCode punchKey = KeyCode.Mouse1; // Botón derecho del ratón
    public bool isPunch = false;

    void Update()
    {
        if (Input.GetKeyDown(punchKey) && !isPunch)
        {
            ActivatePunch();
        }
    }

    void ActivatePunch()
    {
        fist.SetActive(true); // Activa el objeto del puño
        fistAnimator.SetTrigger("Punch"); // Activa la animación
        isPunch = true;
        Invoke(nameof(ResetPuch), 0.1f);
    }
    void ResetPuch()
    {
        isPunch = false;
    }
}