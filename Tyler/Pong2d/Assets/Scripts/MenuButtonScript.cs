using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{

    public void OnClicked()
    {
        SceneManager.LoadScene("MainGame");
    }

}