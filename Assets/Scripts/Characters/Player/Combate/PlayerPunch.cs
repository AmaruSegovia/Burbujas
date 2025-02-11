using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public GameObject fist; // Referencia al objeto del puño
    public Animator fistAnimator; // Referencia al Animator del puño
    public KeyCode punchKey = KeyCode.Mouse1; // Botón derecho del ratón

    void Update()
    {
        if (Input.GetKeyDown(punchKey))
        {
            ActivatePunch();
        }
    }

    void ActivatePunch()
    {
        fist.SetActive(true); // Activa el objeto del puño
        MessageFloatingUI.Instance.ShowMessage("¡Has recogido una moneda!");
        fistAnimator.SetTrigger("Punch"); // Activa la animación
    }
}
