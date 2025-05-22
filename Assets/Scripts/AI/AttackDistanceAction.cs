using System;
using System.Collections;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackDistance", story: "[Agent] charges at [target]", category: "Action", id: "880efb4321d6c58db52a57ef7499cefd")]
public partial class AttackDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    //[SerializeReference] public BlackboardVariable<Animator> myAnimator;

    [SerializeReference] public BlackboardVariable<float> Speed = new BlackboardVariable<float>(2.0f);
    //[SerializeReference] public BlackboardVariable<Animator> myAnimator = new BlackboardVariable<Animator>();

    private Animator myAnimator;
    private Vector3 targetPos;
    private bool first = true;

    protected override Status OnStart()
    {
        Debug.Log("start do charge");
        if (Agent.Value == null || Target.Value == null)
        {
            Debug.Log("Charge falhou");
            return Status.Failure;
        }


        return Initialize();
    }

    protected override Status OnUpdate()
    {
        Debug.Log("update do charge");


        Vector3 agentPosition = Agent.Value.transform.position;
        Vector3 toDestination;
        float speed = Speed;
        toDestination = targetPos - agentPosition;
        toDestination.z = 0.0f;
        toDestination.Normalize();
        agentPosition += toDestination * (speed * Time.deltaTime);
        Agent.Value.transform.position = agentPosition;

        if (Vector3.Distance(Agent.Value.transform.position, targetPos) < 1)
            return Status.Success;
        first = false;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        myAnimator.SetBool("PrepareCharge", false);
        //myAnimator.SetBool("Charge", false);
    }

    protected override void OnDeserialize()
    {
        Initialize();
    }

    private Status Initialize()
    {
        Debug.Log("initialize do charge");
        myAnimator = Agent.Value.GetComponentInChildren<Animator>();
        myAnimator.SetBool("Charge", true);
        targetPos = Target.Value.transform.position;

        return Status.Running;
    }
}

