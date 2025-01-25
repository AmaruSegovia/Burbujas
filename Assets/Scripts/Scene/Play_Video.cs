using System.Collections;
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

        // Verificar si el objeto está activo antes de iniciar la corutina
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(PlayNextVideo());
        }
        else
        {
            // Si el objeto está inactivo, activarlo temporalmente
            gameObject.SetActive(true);
            StartCoroutine(PlayNextVideo());
        }
    }

    IEnumerator PlayNextVideo()
    {
        if (currentVideoIndex >= videos.Length) // Si ya no hay mas videos, cargar la siguiente escena
        {
            GoToGameScene();
            yield break;
        }

        // Detiener el video anterior, si hay uno
        if (currentVideoIndex > 0)
        {
            SceneLoad.Instance.LoadAnimation();
            yield return new WaitForSeconds(1f);

            videos[currentVideoIndex - 1].Pause();  // Pausa el video actual
            videos[currentVideoIndex - 1].gameObject.SetActive(false); // Ocultar el video 
        }

        // Reproducir el video actual
        videos[currentVideoIndex].gameObject.SetActive(true); // Mostrar el video
        videos[currentVideoIndex].Play(); // Reproducir el video

        // Esperar la duración del video actual
        yield return new WaitForSeconds(videoDurations[currentVideoIndex]);

        // Incrementa el índice para el siguiente video
        currentVideoIndex++;

        // Si hay más videos, continúa con la siguiente llamada
        if (currentVideoIndex < videos.Length)
        {
            StartCoroutine(PlayNextVideo());
        }
        else
        {
            // Si ya no hay más videos, llama a la función para cargar la escena
            GoToGameScene();
        }
    }

    void GoToGameScene()
    {
        SceneLoad.Instance.LoadSceneAnimation(NextScene); // Cambia a la escena de juego
    }
}
