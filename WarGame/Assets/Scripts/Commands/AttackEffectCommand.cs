using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectCommand : Command
{
    GameObject _attacker;
    Vector3 _attackLocation;
    Vector3 _victimLocation;
    GameObject _attackPrefab;

    public AttackEffectCommand(GameObject attacker, Vector3 attackLocation, Vector3 victimLocation, GameObject attackPrefab)
    {
        _attacker = attacker;
        _attackLocation = attackLocation;
        _victimLocation = victimLocation;
        _attackPrefab = attackPrefab;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        Vector3 direction = (_victimLocation - _attackLocation).normalized;
        direction.z -= 0.2f;

        Vector3 targetVector = _attackLocation + (direction * 2.0f);

        // move back
        ((new MoveGameObjectCommand(new GameObject[] { _attacker },
            //_attackLocation + (direction * -0.5f),
            new Vector3(_attackLocation.x,
                        _attackLocation.y,
                        targetVector.z),
            0.2f / GlobalSettings.Instance.TimeMultiplier))).InsertAtFront();
        // move forward
        ((new MoveGameObjectCommand(new GameObject[] { _attacker },
            targetVector,
            0.05f / GlobalSettings.Instance.TimeMultiplier))).InsertAtFront();

        Command.CommandExecutionComplete();
    }
}

//Using effects:

//Just drag and drop prefab of effect on scene and use that :)
//If you want use effects in runtime, use follow code:

//"Instantiate(prefabEffect, position, rotation);"

//Using projectile collision event:
//void Start ()
//{
//    var tm = GetComponentInChildren<RFX4_TransformMotion>(true);
//    if (tm != null) tm.CollisionEnter += Tm_CollisionEnter;
//}

//private void Tm_CollisionEnter(object sender, RFX4_TransformMotion.RFX4_CollisionInfo e)
//{
//    Debug.Log(e.Hit.transform.name); //will print collided object name to the console.
//}
//------------------------------------------------------------------------------------------------------------------------------------------
//Effect modification:

//All effects includes helpers scripts(collision behaviour, light/shader animation etc) for work out of box.
//Also you can add additional scripts for easy change of base effects settings.


//RFX4_EffectSettingColor - for change color of effect (uses HUE color). Can be added on any effect.
//RFX4_EffectSettingPhysxForce - for change physx force of effects with rocks levitation (and for effect 6 with black hole force)
//RFX4_EffectSettingProjectile - for change projectile fly distance, speed and collided layers.
//RFX4_EffectSettingVisible - for change visible status of effect using smooth fading by time.
