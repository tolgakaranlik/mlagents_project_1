using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class GameAgent : Agent
{
    public Renderer Ground;
    public Transform Target;
    public float Speed = 1.0f;

    public override void OnActionReceived(ActionBuffers actions)
    {
        float x = actions.ContinuousActions[0];
        float z = actions.ContinuousActions[1];

        transform.position += new Vector3(x, 0, z) * Speed * Time.deltaTime;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);
        sensor.AddObservation(transform.localPosition.z);
        sensor.AddObservation(Target.localPosition.x);
        sensor.AddObservation(Target.localPosition.y);
        sensor.AddObservation(Target.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            AddReward(-1);
            EndEpisode();
            Ground.material.color = Color.red;
        }

        if (other.tag == "Target")
        {
            Debug.Log("Hit the target!");
            AddReward(1);
            EndEpisode();
            Ground.material.color = Color.green;
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(1, 9), 1.5f, Random.Range(-8, 8));
    }
}
