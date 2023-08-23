using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader3 : MonoBehaviour
{
    public BowlingAgent agent;
    
    // Update is called once per frame
    void Update()
    {
        if (agent.points > 30 && agent.points <= 40)
        {
            //passa al livello 4
            SceneManager.LoadSceneAsync(3);
        }
    }
}
