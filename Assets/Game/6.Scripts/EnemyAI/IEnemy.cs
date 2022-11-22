using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

namespace DredgeAttack
{
    public enum DredgeAttackVariations{ Noone, Waves, Puke, Hungry };
}

namespace Action__
{
    public enum ActionStatus { Running, Success, Fail };
    public enum BehaviourState { Patrol, Chase, Die, Attack, Rest, Banishing, Banished, Onslaught, Hidden, Observing };
    public enum BehaviourNodes { Wait, SeeingPlayer, SeekingPlayer, RecievingDamage, Eat, Kill, Attack };
}
public interface IEnemy
{

    public void AIActionExecuting(BehaviourState state);
}