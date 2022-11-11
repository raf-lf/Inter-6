using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

namespace Action__
{
    public enum ActionStatus { Running, Success, Fail };
    public enum BehaviourState { Patrol, Chase, Die, Attack, Rest, Burn, Banishing, Banished, Onslaught, Hidden };
    public enum BehaviourNodes { Wait, SeeingPlayer, SeekingPlayer, RecievingDamage, Eat, Kill, Attack };
}
public interface IEnemy
{

    public void AIActionExecuting(BehaviourState state);
}