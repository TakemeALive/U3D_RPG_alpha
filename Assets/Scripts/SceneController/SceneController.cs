using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script exists in the Persistent
public class SceneController : MonoBehaviour {

	public string startingSceneName = "MainScene";
	public string playerSpawnPoint = "PlayerSpawnPoint";

	private IEnumerator Start(){
		yield return StartCoroutine (LoadSceneAndSetActive (startingSceneName));
	}

	private IEnumerator SwitchScene(string SceneName){



		yield return null;
	}

	private IEnumerator LoadSceneAndSetActive (string sceneName)
    {
        // Allow the given scene to load over several frames and add it to the already loaded scenes (just the Persistent scene at this point).
        yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);

        // Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
        Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

        // Set the newly loaded scene as the active scene (this marks it as the one to be unloaded next).
        SceneManager.SetActiveScene (newlyLoadedScene);
    }
}
