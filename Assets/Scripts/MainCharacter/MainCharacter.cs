﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    // Info of player
    [Header("Data of the ball")]
    public Rigidbody m_rigidbody = default;
    public InputMobile m_inputMobile;

    [Header("All of Data for the ball need to change")]
    public Renderer ballRenderer;
    public Renderer shapeRenderer;
    public GameObject ballExplosion;
    public GameObject particleMoving;

    [Header("Animator")]
    public Animator animator;

    [Header("Set time particle when player moving")]
    public float timeParMoving = 0.5f; 

    [Header("Get data the ball from Scriptable object")]
    public ScriptTheBall scriptTheBall; 
    public float moveSpeed = 0f; 
    public float angleSpeed = 0f;
    
    [Header("State of the ball")]
    public CharacterMove m_characterMove;
    // public BallPower m_ballPower;
    // public BallGravity m_ballGravity;
    public CharacterNone m_characterNone;

    [Header("Slider process")]
    public Slider sliderProcess;
    public Image sliderImage;
    public float timeProcessFinish = 0f;
    public float currentTimeProcess = 0;

    //
    public float speedIncrease = 0f;
    public float timerSpeedUp = 0f;
    public float engineForce = 0f;
    public float turnSpeed = 0f;

    //
    public bool isFirstTriggerPower = false;

    private StateCharacter currentState;
    public StateCharacter CurrentState { get => currentState; set => currentState = value; }

    #region UNITY
    private void Start()
    {
        if(m_rigidbody == null)
            m_rigidbody = this.GetComponent<Rigidbody>();

        LoadData();
        ChangeState(m_characterMove);
    }

    private void FixedUpdate()
    {
        if(currentState != null)// && SceneManager.GetInstance().IsInGame())
        {
            currentState.UpdateState();
        }
    }
    #endregion

    private void LoadData()
    {
        this.moveSpeed = scriptTheBall.moveSpeed;
        this.angleSpeed = scriptTheBall.angleSpeed;
        //
        this.speedIncrease = scriptTheBall.speedIncrease;
        this.timerSpeedUp = scriptTheBall.timerSpeedUp;
        this.engineForce = scriptTheBall.engineForce;
        this.turnSpeed = scriptTheBall.turnSpeed;

        sliderProcess.gameObject.SetActive(false);
    }


    public void ChangeState(StateCharacter newState)
    {
        if(currentState != null)
        {
            currentState.EndState();
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.Owner = this;
            currentState.StartState();
        }
    }

    
    public void Reset()
    {//
        //
        m_characterMove.Reset();
        // m_ballPower.Reset();

        isFirstTriggerPower = false;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //this.GetComponentInChildren<BallPower>().Reset();
        
        ChangeState(m_characterMove);
    }


    public bool isStateBallMove()
    {
        return currentState == m_characterMove;
    }
}