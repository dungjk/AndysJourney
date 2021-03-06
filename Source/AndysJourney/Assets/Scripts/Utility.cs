using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utility
{
    public static float TimeByFrame(float frameNumber, float frameRate)
    {
        return (1f / frameRate) * frameNumber;
    }

    public static float FrameByTime(float time, float frameRate)
    {
        return time * frameRate;
    }


    public static void RotateAround(Rigidbody rigid, Transform own, Transform target, Vector3 axis, float targetMagnitude, float velocity)
    {
        own.RotateAround(target.position, axis, velocity * Time.deltaTime);
    }

    public static Vector3 CalculateVelocity(Quaternion rotation, float ambientSpeed)
    {
        var forward = Vector3.forward;
        forward = rotation * forward;
        var velocity = forward * ambientSpeed;
        return velocity;
    }

    public static void ActiveLayer(Animator animator, string layerName)
    {
        for (var i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }

    public static IEnumerator JumpToDestination(Transform owner, Vector3 source, Vector3 destination, System.Action toBeAtDestination = null)
    {
        var percent = .0f;
        while (percent <= 1f)
        {
            percent += Time.deltaTime * 2;
            var currentPos = Vector2.Lerp(source, destination, percent);
            var trajectoryHeight = Random.Range(.09f, .175f);
            currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(percent) * Mathf.PI);
            owner.position = currentPos;
            yield return null;
        }
        if (toBeAtDestination != null)
            toBeAtDestination();
    }

    public static void JumpToDestination(Transform owner, Vector3 source, Vector3 destination, float trajectoryHeight, float percent, System.Action toBeAtDestination = null)
    {
        var currentPos = Vector2.Lerp(source, destination, percent);
        currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(percent) * Mathf.PI);
        owner.position = currentPos;
        if (toBeAtDestination != null)
            toBeAtDestination();
    }
}