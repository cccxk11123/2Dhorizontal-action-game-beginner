using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvent : MonoBehaviour
{
    public static UnityAction<GameObject, float> charactertookDamage;
    public static UnityAction<GameObject, float> characterHealed;
}
