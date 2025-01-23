using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public static SceneLoad Instance { get; private set ;} 
    [SerializeField] private float transitionTime;
    private Animator transitionAnimator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        transitionAnimator = GetComponentInChildren<Animator>();
    }

    public void LoadNextScene (){
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadingScene(nextScene));
    }

    public void LoadPreviousScene (){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int previousScene = currentScene - 1;
        if (previousScene >= 0){
            StartCoroutine(LoadingScene(previousScene));
        } else Debug.Log("Ya estas en la prmera escena, no puedes volver");
    }

    public IEnumerator LoadingScene(int sceneIndex){
        transitionAnimator.SetTrigger("startTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
