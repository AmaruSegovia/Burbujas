using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public GameObject fist; // Referencia al objeto del pu�o
    public Animator fistAnimator; // Referencia al Animator del pu�o
    public KeyCode punchKey = KeyCode.Mouse1; // Bot�n derecho del rat�n
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
        fist.SetActive(true); // Activa el objeto del pu�o
        fistAnimator.SetTrigger("Punch"); // Activa la animaci�n
        isPunch = true;
        Invoke(nameof(ResetPuch), 0.1f);
    }
    void ResetPuch()
    {
        isPunch = false;
    }
}