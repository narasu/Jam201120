using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateScript_Death : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("r")) {
 SceneManager.LoadScene("SkeletoniFinal");

        }

        if (Input.GetKey("m")) {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
