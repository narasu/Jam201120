using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateScript_TutorialScreen : MonoBehaviour
{
    public int time_in_tutorial = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( time_in_tutorial <= Time.timeSinceLevelLoad)
        {
            SceneManager.LoadScene("SkeletoniFinal");
        }
    }
}
