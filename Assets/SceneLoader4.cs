using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader4 : MonoBehaviour
{
    public BowlingAgent agent;
   
    // Update is called once per frame
    void Update()
    {
        if (agent.points > 40 && agent.points <= 50)
        {
            //passa al livello 5
            SceneManager.LoadSceneAsync(4);
        }
    }
}
