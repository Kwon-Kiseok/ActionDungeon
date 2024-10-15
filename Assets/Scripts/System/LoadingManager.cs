using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Text;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    
    [SerializeField] private string sceneName;

    private void Start() {
        StartCoroutine(SceneLoad());
    }

    private IEnumerator SceneLoad()
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        ao.allowSceneActivation = false;

        StringBuilder sb = new StringBuilder();

        while(!ao.isDone)
        {
            yield return null;

            if(ao.progress >= 0.9f)
            {
                loadingText.text = "100%";
                ao.allowSceneActivation = true;
            }
            else
            {
                sb.Clear();
                sb.Append(ao.progress * 100f).Append("%");
                loadingText.text = sb.ToString();
            }
        }
    }
    
}
