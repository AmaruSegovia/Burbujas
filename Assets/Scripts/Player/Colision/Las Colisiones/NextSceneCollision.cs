using UnityEngine;

public class NextSceneCollision : ICollision
{
    public void HandleCollision (Collider2D other)
    {
        SceneLoad.Instance.LoadNextScene();
    }
}
