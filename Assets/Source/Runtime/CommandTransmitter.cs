using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Source.Runtime
{
    public class CommandTransmitter : SerializedMonoBehaviour
    {
        public event Action<BotCommand> Commanded;
        
        [SerializeField]
        private Dictionary<string, BotCommand> _commandDictionary = new();
        [SerializeField]
        private TMP_InputField _inputField;
        [SerializeField]
        private Transform _goodSignalPrefab;
        [SerializeField]
        private Transform _wrongSignalPrefab;
        [SerializeField]
        private float _signalSpeed;
        [SerializeField]
        private float _signalDistanceToReach;

        private PlayerInput _playerInput;
        private Transform _botTransform;

        private string _lastCommand;

        private void Awake()
        {
            _botTransform = FindObjectOfType<BotCompositionRoot>().transform;
            _playerInput = new PlayerInput();
        }

        private void Start()
        {
            _inputField.text = "";
            _playerInput.Player.Enter.performed += _ => ProcessInputField();
            _playerInput.Player.PreviousCommand.performed += _ => SetupLastCommand();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Update()
        {
            _inputField.ActivateInputField();
        }

        private void SetupLastCommand()
        {
            if(_lastCommand != null)
                _inputField.text = _lastCommand;
        }
        
        private void ProcessInputField()
        {
            var input = _inputField.text;
            input = input.ToLower().Trim(' ');
            _lastCommand = input;
            _inputField.text = "";
            if (_commandDictionary.ContainsKey(input))
            {
                CreateCommandSignal(input, _goodSignalPrefab);
            }
            else
            {
                CreateCommandSignal("stop", _wrongSignalPrefab);
            }
        }

        private void CreateCommandSignal(string input, Transform signalPrefab)
        {
            var signal = Instantiate(signalPrefab);
            signal.transform.position = Camera.main.ScreenToWorldPoint(transform.position);
            StartCoroutine(SignalMove(signal, () => Commanded?.Invoke(_commandDictionary[input])));
        }

        private IEnumerator SignalMove(Transform signal, Action onComplete)
        {
            _signalDistanceToReach = .2f;
            while (Vector3.Distance(signal.position, _botTransform.position) >= _signalDistanceToReach)
            {
                signal.position = Vector3.MoveTowards(signal.position, _botTransform.position,
                    Time.deltaTime * _signalSpeed);
                yield return new WaitForEndOfFrame();
            }
            Destroy(signal.gameObject);
            onComplete?.Invoke();
        }
    }
}