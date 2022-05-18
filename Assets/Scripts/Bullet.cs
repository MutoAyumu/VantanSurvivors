using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    private void OnEnable()
    {

    }
    private void OnBecameInvisible()
    {

        this.gameObject.SetActive(false);
    }
}
