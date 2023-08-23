using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader2 : MonoBehaviour
{
    public BowlingAgent agent;
  
    // Update is called once per frame
    void Update()
    {
        if (agent.points > 20 && agent.points <= 30)
        {
            //passa al livello 3
            SceneManager.LoadSceneAsync(2);
        }
    }
}
