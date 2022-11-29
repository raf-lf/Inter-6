using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

namespace DredgeAttack
{
    public enum DredgeAttackVariations{ Noone, Puke, Submerge, Chomp, Hide };
}

namespace Action__
{
    public enum BehaviourState { Patrol, Chase, Die, Attack, Rest, Banishing, Banished, Teleport, Hidden, Observing };
}
public interface IEnemy
{

    public void AIActionExecuting(BehaviourState state);
}