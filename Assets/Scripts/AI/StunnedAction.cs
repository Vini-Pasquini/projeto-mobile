using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using static UnityEngine.GraphicsBuffer;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stunned", story: "[Agent] is stunned", category: "Action", id: "329c48ab5dd1b20b1649b50a97f06784")]
public partial class StunnedAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    [SerializeReference] public BlackboardVariable<int> health = new BlackboardVariable<int>(3);

    private Animator myAnimator;

    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        int puzzleResult = GameManager.Instance.GetPuzzleAnswer;
        Debug.Log(puzzleResult);

        if (puzzleResult < 0)
        {
            return Status.Running;
        }

        Debug.Log("Resultado");

        myAnimator.SetBool("Stunned", false);

        if (puzzleResult > 0)
        {
            health.Value--;
            myAnimator.SetBool("Hit", true);
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    protected override void OnDeserialize()
    {
        Initialize();
    }

    private Status Initialize()
    {
        myAnimator = Agent.Value.GetComponentInChildren<Animator>();

        //myAnimator.SetBool("WalkDown", false);
        //myAnimator.SetBool("WalkUp", false);
        myAnimator.SetBool("Hit", false);
        //myAnimator.SetBool("Stunned", true);

        GameManager.Instance.CallBossPuzzle();

        return Status.Running;
    }
}

