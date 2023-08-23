using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public BowlingAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (agent.points == 20)
        {
            //passa al livello 2
            SceneManager.LoadSceneAsync(1);
        }
    }
}
