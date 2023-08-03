namespace CardGame
{
    public enum InputState
    { 
        Kill,
        Talk,
        Swap,
        Quit,
        Unknown
    }
    public static class UserInput
    {
        public static InputState GetInputState(string _inputString)
        {
            string[] userInputParsed = _inputString.Split(' ');
            if (userInputParsed.Length == 2)
            {
                switch (userInputParsed[0].ToLower())
                {
                    case "kill":
                        return InputState.Kill;
                    case "talk":
                        return InputState.Talk;
                    case "swap":
                        return InputState.Swap;
                    default:
                        break;
                }
            }
            else if (userInputParsed.Length == 1)
            {
                switch (userInputParsed[0].ToLower())
                {
                    case "quit":
                    case "q":
                        return InputState.Quit;
                    default:
                        break;
                }
            }
            return InputState.Unknown;
        }

        public static int GetInputParam(string _inputString)
        {
            string[] userInputParsed = _inputString.Split(' ');
            if (userInputParsed.Length == 2)
            {
                int param;
                if (int.TryParse(userInputParsed[1], out param))
                {
                    return param;
                }
                else
                {
                    return -1;
                }
            }
            return -1;
        }
    }
}
