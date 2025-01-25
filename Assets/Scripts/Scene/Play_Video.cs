using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Play_Video : MonoBehaviour
{
    // Array de VideoPlayers y de duraciones
    public VideoPlayer[] videos;
    public float[] videoDurations;

    private int currentVideoIndex = 0;

    public int NextScene;

    void Start()
    {
        if (videos.Length == 0 || videoDurations.Length == 0 || videos.Length != videoDurations.Length)
        {
            Debug.LogError("Asegúrate de que los arrays de videos y duraciones sean válidos y tengan el mismo tamaño.");
            return;
        }

        // Configura los videos para que no se reproduzcan en loop
        foreach (var video in videos)
        {
            video.isLooping = false;
            video.gameObject.SetActive(false); // Asegúrate de que todos los videos estén inicialmente ocultos
        }

        // Reproduce el primer video
        PlayNextVideo();
    }

    void PlayNextVideo()
    {
        if (currentVideoIndex >= videos.Length) // Si ya no hay más videos, carga la siguiente escena
        {
            GoToGameScene();
            return;
        }

        // Detiene el video anterior, si hay uno
        if (currentVideoIndex > 0)
        {
            videos[currentVideoIndex - 1].Stop();
            videos[currentVideoIndex - 1].gameObject.SetActive(false);
        }

        // Reproduce el video actual
        videos[currentVideoIndex].gameObject.SetActive(true); // Muestra el video
        videos[currentVideoIndex].Play(); // Reproduce el video

        // Incrementa el índice para el siguiente video
        currentVideoIndex++;

        // Establece el siguiente video para que se reproduzca después del tiempo correspondiente
        Invoke(nameof(PlayNextVideo), videoDurations[currentVideoIndex - 1]); // Llama al siguiente video después del tiempo de duración
    }

    void GoToGameScene()
    {
        SceneLoad.Instance.LoadSceneAnimation(NextScene); // Cambia a la escena de juego
    }
}
