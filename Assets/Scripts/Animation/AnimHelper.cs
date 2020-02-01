using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LL.Events;

public class AnimHelper : MonoBehaviour
{

    public Animator Anim { get; private set; }
    private Action callback;

    private Effect currentEffect;

    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
    }



    public void PlayWalk()
    {
        if (Anim == null)
            return;

        if (Anim.GetBool("Walking") == true)
            return;

        Anim.SetBool("Walking", true);
    }

    public void StopWalk()
    {
        if (Anim == null)
            return;

        if (Anim.GetBool("Walking") == false)
            return;

        Anim.SetBool("Walking", false);
    }


    public void PlayOrStopAnimBool(string boolName, bool play = true)
    {
        if (Anim == null)
            return;

        if (Anim.GetBool(boolName) == false && play == false)
            return;

        if (Anim.GetBool(boolName) == true && play == true)
            return;

        Anim.SetBool(boolName, play);
    }

    public bool IsInState(string stateName) {
        return Anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public bool PlayAnimTrigger(string trigger, Effect effect = null)
    {
        if (Anim == null)
            return false;


        if (string.IsNullOrEmpty(trigger) == true)
            return false;

        try
        {
            Anim.SetTrigger(trigger);
            currentEffect = effect;
            
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(gameObject.name + " Could not play an animation: " + e);
            return false;
        }
    }

    public AnimatorStateInfo GetCurrentAnimatorStateInfo(int index) {
        return Anim.GetCurrentAnimatorStateInfo(index);
    }

    public void SetFloat(string floatName, float value) {
        Anim.SetFloat(floatName, value);
    }
    public void PlayParticleEffect(string particleName)
    {
        ParticleSystem p;

        Transform t = transform.Find("VFX/" +particleName);


        if(t == null)
        {
            Debug.Log("Couldn't find transform " + particleName);
            return;
        }

        p = t.GetComponent<ParticleSystem>();

        if(p == null)
        {
            Debug.Log("Couldn't find particles");
            return;
        }

        p.Play();
        
    }


    public void SetAnimEventAction(Action callback)
    {
        if(this.callback != callback)
            this.callback = callback;
    }

    public void RecieveAnimEvent(AnimationEvent param)
    {
        //Debug.Log("Recieving " + param.stringParameter /*+ " from anim event on " + gameObject.name*/);

        if (this.callback != null)
            callback();

        SendEffectDeliveryEvent(param);
    }

    public void BeginEffectDelivery() {

        if(currentEffect != null) {

            Debug.Log("Recieving Anim Event for " + currentEffect.ParentAbility.abilityName);

            currentEffect.BeginDelivery(currentEffect.weaponDelivery);
            currentEffect = null;
        }

    }


    private void SendEffectDeliveryEvent(AnimationEvent param)
    {

        string[] names = param.stringParameter.Split(',');

        if (names.Length < 2)
            return;

        Ability targetAbility = GameManager.GetAbilityByOwner(gameObject, names[0]);

        if(targetAbility == null)
        {
            //Debug.LogError("Anim helper couldn't find an ability with name: " + param.stringParameter);
            return;
        }

        Effect targetEffect = targetAbility.EffectManager.GetEffectByName(names[1]);

        if (targetEffect == null)
        {
            //Debug.LogError("Anim helper couldn't find an effect on " + targetAbility.abilityName + " with name: " + names[1]);
            return;
        }

        Debug.Log("Begining Delivery for " + targetAbility.abilityName);
        targetEffect.BeginDelivery(targetEffect.weaponDelivery);

    }

}
