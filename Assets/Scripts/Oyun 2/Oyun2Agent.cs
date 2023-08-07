using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Oyun2Agent : Agent
{
    public float Speed = 10f;
    public Transform Target;
    public GameObject[] Coins;
    public Transform[] SpawningPoints;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);
        sensor.AddObservation(transform.localPosition.z);
        sensor.AddObservation(Target.localPosition.x);
        sensor.AddObservation(Target.localPosition.y);
        sensor.AddObservation(Target.localPosition.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float x = actions.ContinuousActions[0];
        float z = Mathf.Abs(actions.ContinuousActions[1]);

        transform.localPosition += new Vector3(x, 0, z) * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            AddReward(-1000);
            EndEpisode();
        } else if(other.tag == "Coin")
        {
            other.gameObject.SetActive(false);

            // En çok altın toplayacak şekilde ajanlarımızı
            // eğitmeye yarayan ödüldür
            AddReward(100);
        } else if(other.tag == "Finish")
        {
            AddReward(10000);
            EndEpisode();
        }

        // En kısa yoldan gitmek için ajanlarımızı eğitmeye yarayan ödüldür
        // AddReward(-1);
    }

    public override void OnEpisodeBegin()
    {
        // 1.Toplanan altınları platforma geri yerleştirmek
        for (int i = 0; i < Coins.Length; i++)
        {
            Coins[i].SetActive(true);
        }

        // 2.Ajanımızın spawning pointlerden herhangi birinde rasgele spawnlanması
        transform.localPosition = SpawningPoints[UnityEngine.Random.Range(0, SpawningPoints.Length)].localPosition;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
