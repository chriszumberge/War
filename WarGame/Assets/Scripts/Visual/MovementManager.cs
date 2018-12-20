using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    public static MovementManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Rotate(GameObject[] cards, Quaternion endRotation, float duration)
    {
        StartCoroutine(RotateObjectsCoroutine(cards, endRotation, duration));
    }

    IEnumerator RotateObjectsCoroutine(GameObject[] cards, Quaternion endRotation, float duration)
    {
        List<RotationDefinition> rotations = new List<RotationDefinition>();
        foreach (var card in cards) rotations.Add(new RotationDefinition { obj = card, start = card.transform.rotation, end = endRotation });

        float time = 0;
        while (time < 1.0f)
        {
            time += Time.deltaTime / duration;
            foreach (var r in rotations)
            {
                r.obj.transform.rotation = Quaternion.Slerp(r.start, r.end, Mathf.SmoothStep(0.0f, 1.0f, time));
                //r.obj.transform.rotation = Quaternion.Slerp(r.start, r.end, time);
            }
            yield return new WaitForFixedUpdate();
        }

        Command.CommandExecutionComplete();
    }

    class RotationDefinition
    {
        public GameObject obj { get; set; }
        public Quaternion start { get; set; }
        public Quaternion end { get; set; }
    }

    public void Move(GameObject[] objects, Vector3[] positions, float duration)
    {
        StartCoroutine(MoveObjectCoroutine(objects, positions, duration));
    }

    IEnumerator MoveObjectCoroutine(GameObject[] objects, Vector3[] positions, float duration)
    {
        List<MovementDefinition> movements = new List<MovementDefinition>();
        for (int i = 0; i < objects.Length; i++)
        {
            movements.Add(new MovementDefinition
            {
                obj = objects[i],
                start = objects[i].transform.position,
                end = positions[i]
            });
        }

        float time = 0;
        while (time < 1.0f)
        {
            time += Time.deltaTime / duration;
            foreach (var movement in movements)
            {
                movement.obj.transform.position = Vector3.Lerp(movement.start, movement.end, Mathf.SmoothStep(0.0f, 1.0f, time));
                //card.transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, time));
            }
            yield return new WaitForFixedUpdate();
        }

        Command.CommandExecutionComplete();
    }

    class MovementDefinition
    {
        public GameObject obj { get; set; }
        public Vector3 start { get; set; }
        public Vector3 end { get; set; }
    }
}
