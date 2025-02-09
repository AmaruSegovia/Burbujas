using UnityEngine;
using UnityEngine.UI;

public class PlayerFace : MonoBehaviour
{
    private Slider slider;
    private Animator animator;

    void Start(){
        slider = GetComponentInParent<Slider>();
        if (slider == null){ 
            Debug.LogWarning("No se encontro el padre Slider");
            return;
        }
        animator = GetComponent<Animator>();
        if (animator == null){ 
            Debug.LogWarning("No tienes un animator");
            return;
        }

        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnSliderValueChanged(float value){
        if (value >= 75f){
            ChangeImage(3); // Cambiando a la animacion "Demencia"
        }
        else if (value >= 50f){
            ChangeImage(2); // Cambiando a la animacion
        }
        else if(value >= 25f){
            ChangeImage(1); // Cambiando a la animacion
        }
        else{
            ChangeImage(0); //Cambiando a la animacion
        }
    }

    public void ChangeImage(int index){
        animator.SetInteger("Face", index);
    }
}
