using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class RestartGame : MonoBehaviour
{
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
