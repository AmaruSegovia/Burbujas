using UnityEngine;

public class SoundModifier : MonoBehaviour
{
    private float ValorAlcohol;
    [SerializeField] private AudioReverbFilter ReverbFilter;
    [SerializeField] private AudioEchoFilter audioEcho;
    [SerializeField] private AudioLowPassFilter lowPassFilter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReverbFilter.enabled = false;
        audioEcho.enabled = false;
        lowPassFilter.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (ScriptGameManager.instance != null)
        {
            ValorAlcohol = ScriptGameManager.instance.PuntosTotalAlcohol;

            if (ValorAlcohol >= 75f)
            {
                ReverbFilter.enabled = true;
                audioEcho.enabled = true;
                lowPassFilter.enabled = true;
            }
            else if (ValorAlcohol >= 50f)
            {
                ReverbFilter.enabled = true;
                audioEcho.enabled = true;
                lowPassFilter.enabled = false;
            }
            else if (ValorAlcohol >= 25f)
            {
                ReverbFilter.enabled = true;
                audioEcho.enabled = false;
                lowPassFilter.enabled = false;
            }
            else
            {
                ReverbFilter.enabled = false;
                audioEcho.enabled = false;
                lowPassFilter.enabled = false;
            }
        }
        else
        {
            Debug.LogError("ScriptGameManager no está inicializado en la escena.");
        }

    }
}
