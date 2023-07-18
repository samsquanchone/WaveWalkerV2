using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public sealed class GameEnvironment
{
    private static GameEnvironment instance;
    private List<GameObject> checkpoints = new List<GameObject>();
    public List<GameObject> Checkpoints { get { return checkpoints; } }
    public Transform safeSpot;


    //This is used to populate a list with the checkpoints that will be used for the statemachine 
    public static GameEnvironment Singleton
    {
        get
        {
            if (instance == null)
            {
                //Creating instance if instance is null
                instance = new GameEnvironment();

                instance.safeSpot = GameObject.FindGameObjectWithTag("SafeSpot").transform;

                //Populating the list 
                instance.Checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));

                instance.checkpoints = instance.checkpoints.OrderBy(waypoint => waypoint.name).ToList(); //Reording the list to be in order,according to the hiarachy 

            }
            return instance;
        }
    }

}
