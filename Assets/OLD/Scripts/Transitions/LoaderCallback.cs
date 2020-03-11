using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thank you to Code Monkey for providing help in setting up this transition code
// https://www.youtube.com/watch?v=3I5d2rUJ0pE&list=PLVWAe_pcoavOKToRtov7OaM7CcUj0an0I&index=4&t=0s

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
