
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BowlingAgent : Agent
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioSource strike;

    [SerializeField]
    AudioSource spare;

    //riferimemto per la palla
    Rigidbody rBody;

    //lista per le posizioni e rotazioni dei birilli
    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;

    //variabile che serve a visualizzare il punteggio
    public Text punteggio;
    public int points;
    public Text striketxt;

    //conteggio birilli
    public int pinsDown;

    public GameObject pin0;
    public GameObject pin1;
    public GameObject pin2;
    public GameObject pin3;
    public GameObject pin4;
    public GameObject pin5;
    public GameObject pin6;
    public GameObject pin7;
    public GameObject pin8;
    public GameObject pin9;
    public GameObject[] pins;

    public MeshRenderer floor;
    public Material strikeMat;
    public Material spareMat;
    public Material notaskorspMat;
    public Material wallMat;

    public int livello;

    public Collider colPlane;

    //riferimento ostacoli 
    public MeshRenderer meshObst;
    public Collider colObst;
    public MeshRenderer meshObst1;
    public Collider colObst1;
    public MeshRenderer meshObst2;
    public Collider colObst2;
    public MeshRenderer meshObst3;
    public Collider colObst3;
    public MeshRenderer meshObst4;
    public Collider colObst4;


    public void Start()
    {
        rBody = GetComponent<Rigidbody>();

        punteggio.text = "PUNTEGGIO: ";
        points = 0;

        pins = new GameObject[10];
        pins[0] = pin0;
        pins[1] = pin1;
        pins[2] = pin2;
        pins[3] = pin3;
        pins[4] = pin4;
        pins[5] = pin5;
        pins[6] = pin6;
        pins[7] = pin7;
        pins[8] = pin8;
        pins[9] = pin9;

        Debug.Log("HO TROVATO" + pins.Length + "BIRILLI");
        pinPositions = new List<Vector3>();
        pinRotations = new List<Quaternion>();

        foreach (var pin in pins)
        {
            pinPositions.Add(pin.transform.localPosition);
            pinRotations.Add(pin.transform.localRotation);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(1);
        }
    }

    void ResetPins()
    {
        Debug.Log("SONO NEL METODO PER RESETTARE IL BIRILLO");

        for (int i = 0; i < pins.Length; i++)
        {
            var rbPins = pins[i].GetComponent<Rigidbody>();
            rbPins.velocity = Vector3.zero;
            rbPins.angularVelocity = Vector3.zero;
            rbPins.transform.localPosition = pinPositions[i];
            rbPins.transform.localRotation = pinRotations[i];
        }

    }

    public Transform Obstacle1;
    public Rigidbody rball;
    public Transform tball;

    public override void OnEpisodeBegin()
    {
        
        Debug.Log("SONO NEL METODO ON EPISODE BEGIN");
        //resetta la velocità (a zero) della palla quando colpisce i birilli, va fuori o ad inizio partita
        rball.velocity = Vector3.zero;

        tball.localPosition = new Vector3(Random.Range(1.3f, -1.3f), 0f, Random.Range(0f, -12f));
        rball.angularVelocity = Vector3.zero;
        pinsDown = 0;
    }

    public Transform Target;
    public Transform Obstacle;

    public override void CollectObservations(VectorSensor sensor)
    {
        //posizioni target, agente e ostacolo
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Obstacle.transform.localPosition);

        //size dell'ostacolo
        sensor.AddObservation(meshObst.bounds.size);

        //velocità dell'agente
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    public float forceMultiplier;

    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];

        rBody.AddForce(controlSignal * forceMultiplier);
    }

    IEnumerator ResetWithDelay()
    {
        Debug.Log("SONO NEL METODO AD ASPETTARE");
        yield return new WaitForSeconds(0.0f);        
        ResetPins();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("checkpoint"))
        {
            Debug.Log("Collisione con CHECKPOINT");
            SetReward(1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BIRILLI TROVATI: " + pins.Length);
        if (collision.gameObject.CompareTag("Finish"))           
        {
            for (int i = 0; i < pins.Length; i++)
            {
                if (pins[i].transform.localRotation != pinRotations[i])
                {
                    pinsDown++;
                    Debug.Log("BIRILLI CADUTI:" + pinsDown);
                }
            } 
        }
        else if (collision.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("CollisionDetected: MURO");
            AddReward(-5.0f);
            floor.material = wallMat;
            ResetPins();
            EndEpisode();
        } 
        else if (collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("Collisione con OSTACOLO");
            AddReward(-2.0f);
            floor.material = wallMat;
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("ground"))
        {
            audioSource.Play();
        }

       
        if (points < 100)
        {
            livello = 1;
            colObst.enabled = false;
            meshObst.enabled = false;
            colObst1.enabled = false;
            meshObst1.enabled = false;
            colObst2.enabled = false;
            meshObst2.enabled = false;
            colObst3.enabled = false;
            meshObst3.enabled = false;
            colObst4.enabled = false;
            meshObst4.enabled = false;
            striketxt.text = "STRIKE!!!, LIVELLO "+ livello + " SUPERATO";
            //SceneManager.LoadScene(1);
        }/*
        else if (points>=10 && points<100)
        {
            livello = 2;

            colObst.enabled = true;
            meshObst.enabled = true;
            colObst1.enabled = true;
            meshObst1.enabled = true;
            colObst2.enabled = true;
            meshObst2.enabled = true;
            colObst3.enabled = true;
            meshObst3.enabled = true;
            colObst4.enabled = true;
            meshObst4.enabled = true;
            striketxt.text = "STRIKE!!!, LIVELLO " + livello + " SUPERATO";
        }                  
        else if(points>30 && points <= 50)
        {
            livello = 3;
            //forceMultiplier = Random.Range(15f, 20f);
            //rBody.mass = Random.Range(3f,5f);
            //striketxt.text = "STRIKE!!!, LIVELLO " + livello + " SUPERATO";
        } 
        else if (points>30 && points<=50)
        {
            livello = 3;
           //meshRampa.enabled = true;
            //colRampa.enabled = true;
            //meshRampaok.enabled = true;
            //colRampaok.enabled = true;
            colObst.enabled = true;
            meshObst.enabled = true;
            colObst1.enabled = true;
            meshObst1.enabled = true;
            colObst2.enabled = true;
            meshObst2.enabled = true;
            colObst3.enabled = false;
            meshObst3.enabled = false;
            colObst4.enabled = false;
            meshObst4.enabled = false;
            striketxt.text = "STRIKE!!!, LIVELLO " + livello + "SUPERATO";
        } 
        //points>70 && points<100
        else if (points>50 && points<=70)
        {
            //meshRampa.enabled = true;
            //colRampa.enabled = true;
            //meshRampaok.enabled = true;
            //colRampaok.enabled = true;
            livello = 5;
            colObst.enabled = true;
            meshObst.enabled = true;
            colObst1.enabled = true;
            meshObst1.enabled = true;
            colObst2.enabled = true;
            meshObst2.enabled = true;
            colObst3.enabled = true;
            meshObst3.enabled = true;
            colObst4.enabled = true;
            meshObst4.enabled = true;
            striketxt.text = "STRIKE!!!, LIVELLO " + livello + "SUPERATO";
        } */
        else if (points >= 100)
        {
            striketxt.text = "YOU WON!";
            points = 0;
        }
  
        if (pinsDown == pins.Length)
        {
            Debug.Log("STRIKE!!!");
            strike.Play();
            points += 10;
            punteggio.text= "PUNTEGGIO:" + points;
            striketxt.text = "STRIKE!!!";
            AddReward(10.0f);
            floor.material = strikeMat;
            StartCoroutine(ResetWithDelay());
            EndEpisode();
        }
        else if (pinsDown == pins.Length - 1)
        {
            Debug.Log("SPARE!!!");
            spare.Play();
            points += 9;
            punteggio.text = "PUNTEGGIO:" + points;
            striketxt.text = "SPARE!!!";
            AddReward(6.0f); //prima era a 9.0
            floor.material = spareMat;
            StartCoroutine(ResetWithDelay());
            EndEpisode();
        }
        else if (pinsDown < pins.Length - 1 && pinsDown != 0)
        {
            Debug.Log("COLPITO UN NUMERO DI BIRILLI <9");
            points += pinsDown;
            punteggio.text = "PUNTEGGIO:" + points;
            striketxt.text = "TRY AGAIN!!!";
            AddReward(0.0f);
            floor.material = notaskorspMat;
            StartCoroutine(ResetWithDelay());
            EndEpisode();
        }
    }
}