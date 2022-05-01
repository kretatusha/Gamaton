using System;
using UnityEngine.SceneManagement;

namespace Source.Runtime
{
    public class CommandReceiver
    {
        public event Action MoveCommanded;
        public event Action JumpCommanded;
        public event Action StopCommanded;
        public event Action FlipCommanded;
        public event Action KillCommanded;
        public event Action ShootCommanded;
        
        private readonly CommandTransmitter _commandTransmitter;

        public CommandReceiver(CommandTransmitter commandTransmitter)
        {
            _commandTransmitter = commandTransmitter;
        }

        public void Init()
        {
            _commandTransmitter.Commanded += OnCommandTransmitted;
        }

        private void OnCommandTransmitted(BotCommand botCommand)
        {
            switch (botCommand)
            {
                case BotCommand.MOVE:
                    MoveCommanded?.Invoke();
                    break;
                case BotCommand.JUMP:
                    JumpCommanded?.Invoke();
                    break;
                case BotCommand.FLIP:
                    FlipCommanded?.Invoke();
                    break;
                case BotCommand.KILL:
                    StopCommanded?.Invoke();
                    KillCommanded?.Invoke();
                    break;
                case BotCommand.STOP:
                    StopCommanded?.Invoke();
                    break;
                case BotCommand.SHOOT:
                    StopCommanded?.Invoke();
                    ShootCommanded?.Invoke();
                    break;
                case BotCommand.EXIT:
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }
}