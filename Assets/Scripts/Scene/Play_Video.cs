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
            Debug.LogError("los arrays de videos y duraciones no son válidos.");
            return;
        }

        // Configura los videos para que no se reproduzcan en loop
        foreach (var video in videos)
        {
            video.isLooping = true;
            video.gameObject.SetActive(false); // que todos los videos esten ocultos
        }

        // Reproducir el primer video
        PlayNextVideo();
    }

    void PlayNextVideo()
    {
        if (currentVideoIndex >= videos.Length) // Si ya no hay mas videos, cargar la siguiente escena
        {
            GoToGameScene();
            return;
        }

        // Detiener el video anterior, si hay uno
        if (currentVideoIndex > 0)
        {
            videos[currentVideoIndex - 1].Pause();  // Pausa el video actual
            videos[currentVideoIndex - 1].gameObject.SetActive(false); // Ocultar el video actual
        }

        // Reproduce el video actual
        videos[currentVideoIndex].gameObject.SetActive(true); // Moestrar el video
        videos[currentVideoIndex].Play(); // Reproducir el video

        // enviar el siguiente video para que se reproduzca después del tiempo estimado
        Invoke(nameof(PlayNextVideo), videoDurations[currentVideoIndex]); // Llama al siguiente video

        // Incrementa el índice para el siguiente video
        currentVideoIndex++;
    }

    void GoToGameScene()
    {
        SceneLoad.Instance.LoadSceneAnimation(NextScene); // Cambia a la escena de juego
    }
}
