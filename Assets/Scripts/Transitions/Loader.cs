using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Thank you to Code Monkey for providing help in setting up this transition code
// https://www.youtube.com/watch?v=3I5d2rUJ0pE&list=PLVWAe_pcoavOKToRtov7OaM7CcUj0an0I&index=4&t=0s

public static class Loader
{
    public enum Scene
    {
        TestEnvironment_Alex,
        Loading,
        MainMenu,
        Level2,
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {   // Set the loader callback action to load the target scene
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback()
    {   // Triggered after the first Update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
