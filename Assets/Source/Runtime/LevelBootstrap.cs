using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LevelBootstrap : MonoBehaviour
{
    [SerializeField]
    private BotCompositionRoot _botCompositionRoot;
    [SerializeField]
    private float _bootstrapDuration;
    [SerializeField]
    private Transform _clawEndPosition;
    private readonly int _clawOpenHash = Animator.StringToHash("Open");
    private Animator _clawAnimator;
    [SerializeField]
    private CinemachineVirtualCamera _cm;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
        _botCompositionRoot.transform.parent = transform;
        _cm.Follow = null;
        _cm.LookAt = null;
        _clawAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        transform.DOMove(_clawEndPosition.position, _bootstrapDuration).onComplete +=
            () => _clawAnimator.SetTrigger(_clawOpenHash);
        _botCompositionRoot.enabled = false;
    }

    public void EnableBot()
    {
        _cm.Follow = _botCompositionRoot.transform;
        _cm.LookAt = _botCompositionRoot.transform;
        _botCompositionRoot.transform.parent = null;
        _botCompositionRoot.enabled = true;
        transform.DOMove(_startPosition, _bootstrapDuration).onComplete += () => DisableClaw();
    }

    private void DisableClaw() => gameObject.SetActive(false);
}