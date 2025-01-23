using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    private Animator transitionAnimator;

    private void Start() {
        transitionAnimator = GetComponentInChildren<Animator>();
    }

    void Update(){
        
            if(transitionAnimator!= null){
                Debug.Log("hay un animador");
            }
        if(Input.GetKeyDown(KeyCode.Space)){
            LoadNextScene();
        }
    }

    public void LoadNextScene (){
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadingScene(nextScene));
    }

    public IEnumerator LoadingScene(int sceneIndex){
        transitionAnimator.SetTrigger("startTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
