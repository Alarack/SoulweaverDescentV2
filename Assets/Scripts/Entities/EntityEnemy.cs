using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LL.FSM;

public class EntityEnemy : Entity
{

    public string defaultState;
    public string aggroState;

    public AIBrain Brain { get; protected set; }
    public AISensor AISensor { get; protected set; }

    protected override void Awake()
    {
        InitStats();
        
        AISensor = GetComponentInChildren<AISensor>();
        if (AISensor != null)
            AISensor.Initialize(this);

        Brain = GetComponent<AIBrain>();
        if (Brain != null)
            Brain.Initialize(this, AISensor);

        base.Awake();
    }


    private void Start()
    {
        //FSM TESTING
        FSMState normalState = FSMManager.GetState(defaultState);
        if (normalState != null)
            EntityFSM.ChangeState(normalState);
        else
        {
            Debug.LogError("Can't find Default state");
        }
    }

    public void Aggro()
    {
        FSMState chaseState = FSMManager.GetState(aggroState);
        if (chaseState != null)
            EntityFSM.ChangeState(chaseState);
    }



    private void Update()
    {
        if(transform.position.y <= -11)
        {
            if (Health != null)
                Health.Die();
            else
                Debug.LogError(entityName + " has no health death manager and is trying to die");

        }
    }




}
