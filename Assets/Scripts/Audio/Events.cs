using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AreaType { CrashArea, IceArea, MilitaryArea, DesertArea };

public class Events : MonoBehaviour, Manhattan.Listener {


    
    public static Events Instance => m_instance;
    private static Events m_instance;
    public Manhattan manhattan;
    public Dropdown instrument;
    public Button variation;
    public Button keyChange, keyReset;
    public Slider tempo;
    public float EnemyLevel = 0;
    
    

    
    

    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;

        
       
        manhattan.Code("@Melody.instr = @Saw.instr");
    }

    void Update () {

        manhattan.Set("@EnemyLevel", EnemyLevel);
        //Debug.Log("Enemy Level is " + EnemyLevel.ToString());
    }

    public void OnRequestVariation() {
        manhattan.Set("Variation", 1);
        variation.interactable = false;
    }

    public void OnRequestKeyChange() {
        manhattan.Set("Transpose", 1);
        keyChange.interactable = false;
    }

    public void OnRequestKeyReset() {
        manhattan.Set("Transpose", 0);
        manhattan.Code("@Transpose.pitch = E-3");
        keyChange.interactable = false;
        keyReset.interactable = false;
    }
    
    public void OnTriggerEnemySpawnSound()
    {
        
    }
    public void OnTriggerStinger(PickUpType pickUpType)
    {
        switch (pickUpType)
        {
            case PickUpType.Ammo:
                Debug.Log("Triggering: " + pickUpType.ToString() + "  Stinger");
                manhattan.Code("Play(@Ammo)");

                break;

            case PickUpType.Health:
                Debug.Log("Triggering: " + pickUpType.ToString() + "  Stinger");
                manhattan.Code("Play(@Health)");
                break;



        }
    }

    public void ChangeZone(AreaType areaType)
    {

        switch (areaType)
        {

            case AreaType.IceArea:
                Debug.Log("Entering: " + areaType.ToString() + "  zone");
                manhattan.Set("@Counter2", 0);
                manhattan.Set("@FadeInTarget", 2);
                manhattan.Set("@FadeOutTarget", 0);
                manhattan.Set("@Engage", 0);
                manhattan.Code("play(@FadeIn)");
                manhattan.Code("play(@FadeOut)");
                break;
        }
    }


    public void StartEnemyArea(AreaType areaType)
    {

        switch (areaType)
        {
            case AreaType.CrashArea:
                Debug.Log("Entering: " + areaType.ToString() + "  Enemy Area");
                manhattan.Set("@Engage", 1);
                break;
        }
    }

    //public void StopEnemyZone(PoolingObjectType poolingObject)
    //{
    //    switch (poolingObject)
    //    {
    //        case PoolingObjectType.CrashZone:
    //            Debug.Log("Stopping: " + poolingObject.ToString() + "  Stinger");
    //            manhattan.Set("@Engage", 0);
    //            break;

    //        case PoolingObjectType.IceZone:
    //            Debug.Log("Stopping: " + poolingObject.ToString() + "  Stinger");
    //            manhattan.Set("@Engage", 0);
    //            break;

    //        case PoolingObjectType.DesertZone:
    //            Debug.Log("Stopping: " + poolingObject.ToString() + "  Stinger");
    //            manhattan.Set("@Engage", 0);
    //            break;
    //        case PoolingObjectType.GrassZone:
    //            Debug.Log("Stopping: " + poolingObject.ToString() + "  Stinger");
    //            manhattan.Set("@Engage", 0);
    //            //manhattan.Code("stop(@BlackHole)");
    //            break;
    //    }
    //}

    public void EngageEnemy(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Range:
                Debug.Log("Engaging: " + attackType.ToString() + "  Enemy");
                manhattan.Set("@Engage", 1);
                manhattan.Code("Loop(@loop1)");
                manhattan.Set("@FadeInTarget", 1);
                manhattan.Code("Play(@Fadein)");            
                break;

            case AttackType.Melee:
                Debug.Log("Engaging: " + attackType.ToString() + "  Enemy");
                manhattan.Set("@Engage", 1);
                manhattan.Code("Loop(@loop1)");
                manhattan.Set("@FadeInTarget", 1); // channel 11 aka goul enemy
                manhattan.Code("play(@FadeIn)");
                break;

           
        }
    }

    public void KilledEnemy(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Range:
                Debug.Log("Killed " + attackType.ToString() + "  Enemy");
                manhattan.Set("FadeOutTarget", 1); 
                manhattan.Code("Play(@FadeOut)");
                break;

            case AttackType.Melee:
                Debug.Log("Killed " + attackType.ToString() + "  Enemy");
                manhattan.Set("FadeOutTarget", 1); // channel 11 aka goul enemy
                manhattan.Code("Play(@FadeOut)");
                break;
        }
    }



    public void Hello()
    {
        
    }
    public void OnTempoChanged(int value)
    {
        manhattan.Set(".tempo", value);
        manhattan.Set("@SpeedUp", 1);
        manhattan.Run("Loop"); // (code to resync drum loop)
    }

    Dropdown.OptionData TubularBells = null;    // 'hidden' Tubular Bells instrument entry

    
    public void OnInput(string message)
    {
        
    }

    


    
    
}
