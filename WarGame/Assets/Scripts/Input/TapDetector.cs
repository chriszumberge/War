using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDetector : MonoBehaviour
{
    //bool _didClick = Input.GetMouseButtonDown(0);
    //bool _didTouch = (Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began);

    Vector3 startingMousePosition;
    float startTouchTime;

    public event EventHandler<ScreenTappedEventArgs> ScreenTapped;

    // Use this for initialization
    void Start()
    {
        startingMousePosition = Vector3.zero;
        startTouchTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startingMousePosition = Input.mousePosition;
            startTouchTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - startTouchTime < 1)
            {
                if ((Input.mousePosition - startingMousePosition).magnitude < 10)
                {
                    //ScreenTapped?.Invoke(this, new ScreenTappedEventArgs(startingMousePosition));
                    if (ScreenTapped != null)
                    {
                        ScreenTapped.Invoke(this, new ScreenTappedEventArgs(startingMousePosition));
                    }

                    foreach (ITapSubscriber subscriber in tapSubscribers)
                    {
                        subscriber.ScreenTapped(startingMousePosition);
                    }

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(startingMousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        foreach (ITapSubscriber subscriber in tapSubscribers)
                        {
                            subscriber.ObjectTapped(hit);
                        }
                    }
                }
            }

            startingMousePosition = Vector3.zero;
            startTouchTime = 0;
        }
    }

    List<ITapSubscriber> tapSubscribers = new List<ITapSubscriber>();

    public void SubscribeToTap(ITapSubscriber tapSubscriber)
    {
        tapSubscribers.Add(tapSubscriber);
    }

    public void UnsubscribeToTap(ITapSubscriber tapSubscriber)
    {
        tapSubscribers.Remove(tapSubscriber);
    }
}

public interface ITapSubscriber
{
    void ScreenTapped(Vector3 screenPoint);

    void ObjectTapped(RaycastHit hit);
}

public class ScreenTappedEventArgs : EventArgs
{
    public Vector3 ScreenPoint { get { return _screenPoint; } }
    Vector3 _screenPoint;

    public ScreenTappedEventArgs(Vector3 screenPoint)
    {
        _screenPoint = screenPoint;
    }
}


//public static List<ITapHandler> SubscribedHandlers = new List<ITapHandler>();
//void Update()
//{
//    if (_didClick || _didTouch)
//    {
//        // Get click or touch position
//        Ray ray = Camera.main.ScreenPointToRay(_didClick ? Input.mousePosition : (Vector3)Input.GetTouch(0).position);
//        RaycastHit hit;
//        if (Physics.Raycast(ray, out hit))
//        {
//            foreach (var tapHandler in SubscribedHandlers)
//            {
//                if (hit.collider.gameObject is tapHandler)
//                {
//                    tapHandler.onTapped();
//                }
//            }

//            // the object identified by hit.transform was clicked
//            // do whatever you want
//            //Debug.Log("Clicked");

//            //Debug.Log("Something Hit");
//            //if (raycastHit.collider.name == "Soccer")
//            //{
//            //    Debug.Log("Soccer Ball clicked");
//            //}

//            ////OR with Tag

//            //if (raycastHit.collider.CompareTag("SoccerTag"))
//            //{
//            //    Debug.Log("Soccer Ball clicked");
//            //}
//        }
//    }