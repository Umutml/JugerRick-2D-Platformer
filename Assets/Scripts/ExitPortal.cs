using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(GoNextLevel());
    }

    IEnumerator GoNextLevel()
    {
        //Play portal or door anim
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(1.5f);
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);      // Current scene +1 go next one
    }
}
