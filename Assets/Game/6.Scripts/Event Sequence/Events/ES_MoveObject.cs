using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_MoveObject : ES_EventBase
{
    public enum MoveTarget { assignedObject, player}
    public MoveTarget moveTarget;
    public bool lookAt;
    public bool waitCompletion;
    [SerializeField] private GameObject assignedObject;
    [SerializeField] private Transform destination;
    [SerializeField] private float speed;

    public override void Play(EventSequence cine)
    {
        base.Play(cine);

        Transform movingTransform = null;

        switch(moveTarget)
        {
            case MoveTarget.assignedObject:
                movingTransform = assignedObject.transform;
                break;
            case MoveTarget.player:
                movingTransform = GameManager.PlayerInstance.transform;
                break;
        }

        StartCoroutine(MoveSequence(movingTransform));
    }

    private IEnumerator MoveSequence(Transform movingTransform)
    {
        if (!waitCompletion)
            StartCoroutine(NextEvent());

        if (lookAt)
            movingTransform.LookAt(new Vector3(destination.position.x, movingTransform.position.y, destination.position.z));

        while (Vector3.Distance(movingTransform.position, destination.position) > 0f)
        {
            movingTransform.position = Vector3.MoveTowards(movingTransform.position, destination.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
        if(waitCompletion)
           StartCoroutine(NextEvent());
    }
}
