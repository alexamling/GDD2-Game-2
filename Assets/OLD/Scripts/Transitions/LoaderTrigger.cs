using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Thank you to Code Monkey for providing help in setting up this transition code
// https://www.youtube.com/watch?v=3I5d2rUJ0pE&list=PLVWAe_pcoavOKToRtov7OaM7CcUj0an0I&index=4&t=0s
public class LoaderTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Capsule")
        {
            switch (SceneManager.GetActiveScene().name.ToString())
            {
                case "TestEnvironment_Alex":
                    Loader.Load(Loader.Scene.Level2);
                    break;
                case "Level2":
                    Loader.Load(Loader.Scene.MainMenu);
                    break;
                default:
                    break;
            }
        }
    }
}
