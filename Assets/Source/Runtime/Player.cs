using System.Collections;
using System.Collections.Generic;
using Source.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _badEndScreenIndex;
    
    public void TakeDamage()
    {
        SceneManager.LoadSceneAsync(_badEndScreenIndex);
    }
}
