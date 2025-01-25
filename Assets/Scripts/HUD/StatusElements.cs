using UnityEngine;

public class StatusElements : MonoBehaviour
{
    public GameObject tabla;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&& tabla != null){
            tabla.SetActive(true);
        }
    }
}
