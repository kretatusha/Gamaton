using System;

namespace Source.Runtime
{
    public class CommandReceiver
    {
        public event Action MoveCommanded;
        public event Action JumpCommanded;
        public event Action StopCommanded;
        public event Action FlipCommanded;
        public event Action KillCommanded;
        
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
                    KillCommanded?.Invoke();
                    break;
                case BotCommand.STOP:
                    StopCommanded?.Invoke();
                    break;
            }
        }
    }
}