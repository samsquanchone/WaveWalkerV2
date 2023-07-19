using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourInterface : MonoBehaviour
{
    public static MonoBehaviourInterface Instance => m_instance;
    private static MonoBehaviourInterface m_instance;

    private void Start()
    {
        m_instance = this;
    }

    //Some of the AI state classes can't inherit from mono behaviour. Therfore this class will act like an interface providing monobehaviour needs for these state classes, e.g. co-routines 
    public void StartRoutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void InstantiateObject(GameObject obj, Transform pos)
    {
        Instantiate(obj, transform);
    }

    public void StopRoutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }
}
