using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class SceneLoader : MonoBehaviour
    {
        public async void LoadScene(string sceneName, Action afterSceneLoader)
        {
            var sceneLoadingProcess = SceneManager.LoadSceneAsync(sceneName);
            while (sceneLoadingProcess.isDone == false)
            {
                await Task.Yield();
            }

            afterSceneLoader?.Invoke();
        }
    }
}
