using UnityEngine;

public class StatusElements : MonoBehaviour
{
    public GameObject tabla;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            TogglePanel();
        }
    }
    public void TogglePanel(){
        if (tabla != null){
            tabla.SetActive(!tabla.activeSelf);
        }
    }
}
