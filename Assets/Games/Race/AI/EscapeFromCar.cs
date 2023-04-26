using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class EscapeFromCar : Agent
{
    [SerializeField] private GameObject _car;
    [SerializeField] private float moveSpeed;
    public override void CollectObservations(VectorSensor sensor)
    {
        float distance = Vector3.Distance(_car.transform.position, transform.position);
        //Debug.Log(distance + " — DISTANCE");
        sensor.AddObservation(distance);

        Vector3 directionToTarget = _car.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        //Debug.Log(angle + " — ANGLE");
        sensor.AddObservation(angle);
        //base.CollectObservations(sensor);
    }


    private void Start()
    {
        StartCoroutine(RewardByTime());
    }
    private IEnumerator RewardByTime()
    {
        yield return new WaitForSeconds(0.25f);
        AddReward(0.25f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 20));
        _car.transform.localPosition = new Vector3(Random.Range(-10, 20), 0, Random.Range(-10, 30));
    }

/*    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
    }*/
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Equals(_car.name))
        {
            AddReward(-10f);
            EndEpisode();
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float rotateY = actions.ContinuousActions[0] * 180f;
        Debug.Log(actions.ContinuousActions[0] + "  " + rotateY);
        transform.rotation = Quaternion.Euler(0f, rotateY, 0f);
    }
    /*
      SetReward(+-1f);
      EndEpisode();
     */
}
