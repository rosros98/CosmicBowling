using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader5 : MonoBehaviour
{
    public BowlingAgent agent;
    public Transform obstacle1;

    public void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 100, 50), "Genera"))
        {
            obstacle1.localPosition = new Vector3(Random.Range(0.90f, -0.90f), 0.0130000003f, 4.90999985f);
        }   
    }
    
    // Update is called once per frame
    void Update()
    {
        if (agent.points > 50 && agent.points <= 60 || agent.points > 60)
        {
            //passa al livello 1
            SceneManager.LoadSceneAsync(0);
        }
    }
}
