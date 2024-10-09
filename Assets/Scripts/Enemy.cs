using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    
    #region Animations
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion
}
