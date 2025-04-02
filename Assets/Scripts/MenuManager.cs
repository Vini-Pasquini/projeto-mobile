using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        //SceneManager.LoadSceneAsync("TestChar");
        //SceneManager.LoadSceneAsync("TestFullUI");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("TestChar");
    }


    public void TestCards()
    {
        SceneManager.LoadScene("TestFullUI");
    }
}
