using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinScreenController : MonoBehaviour
{
    [SerializeField] public string sceneRestart;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            OnBackHub();
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            OnRestart();
        }
    }

    public void EnableGameWinScreen()
    {
        gameObject.SetActive(true);
    }





    public void OnRestart()
    {
        SceneManager.LoadScene(sceneRestart);
    }

    public void OnBackHub()
    {
        SceneManager.LoadScene("Hub");
    }
}
