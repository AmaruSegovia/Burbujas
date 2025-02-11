using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public GameObject fist; // Referencia al objeto del pu�o
    public Animator fistAnimator; // Referencia al Animator del pu�o
    public KeyCode punchKey = KeyCode.Mouse1; // Bot�n derecho del rat�n

    void Update()
    {
        if (Input.GetKeyDown(punchKey))
        {
            ActivatePunch();
        }
    }

    void ActivatePunch()
    {
        fist.SetActive(true); // Activa el objeto del pu�o
        MessageFloatingUI.Instance.ShowMessage("�Has recogido una moneda!");
        fistAnimator.SetTrigger("Punch"); // Activa la animaci�n
    }
}
