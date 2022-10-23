using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnter : MonoBehaviour
{
    public Action<float, Transform, Vector3, float, int> action;

    public void Hurt(float hardTime, Transform sourceTran, Vector3 repelVelocity, float repelTransition, int damageVal)
    {
        action(hardTime, sourceTran, repelVelocity, repelTransition, damageVal);
    }
}
