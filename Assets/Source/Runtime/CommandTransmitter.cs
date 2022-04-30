using System;
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
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void Start()
        {
            _inputField.text = "";
            _playerInput.Player.Enter.performed += _ => ProcessInputField();
            _inputField.ActivateInputField();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void ProcessInputField()
        {
            var input = _inputField.text;
            input = input.ToLower();
            _inputField.text = "";
            if (_commandDictionary.ContainsKey(input))
            {
                Commanded?.Invoke(_commandDictionary[input]);
            }
            else
            {
                Debug.Log("Incorrect command");
            }
            _inputField.ActivateInputField();
        }
    }
}