namespace Assets.Scripts.Graph {
    static class QInstrucionFactory {
        public static string botName;

        public static string GetCode(string type, Value value = Value.No) {
            // instruction with no value
            if (value == Value.No) {
                switch (type) {
                    case "clone":
                        return CloneCode();
                    case "scan":
                        return ScanCode();
                    case "skip":
                        return SkipCode();
                    default:
                        break;
                }
            } // instruction with value
            else {
                switch (type) {
                    case "energy":
                        return EnergyCode(value);
                    case "move":
                        return MoveCode(value);
                    case "sleep":
                        return SleepCode(value);
                    default:
                        break;
                }
            }
            return null;
        }

        public static string GetHeader() {
            string code = "MODULE " + botName + ";\n";
code += @"IMPORT Bot, Math, Random;
VAR bearing, distance, isClone:REAL;

PROCEDURE BatLevel(): REAL;
VAR max, cur: REAL;
BEGIN
    max := Bot.GetMaxBattery();
    cur:= Bot.GetBattery();
RETURN cur / max;
END BatLevel;

BEGIN
WHILE TRUE DO";
            return code;
        }

        public static string GetFooter() {
            return @"
Bot.Sleep(0.4);
END
END " + botName + ".";
        }

        public static string GetEndBlock() {
            return "END";
        }

        public static string GetElse() {
            return "ELSE";
        }

        private static string SleepCode(Value time) {
            float value = 1.0f;
            switch (time) {
                case Value.Low:
                    value = 1.0f/3f;
                    break;
                case Value.Mid:
                    value = 1.0f/3*2f;
                    break;
                case Value.High:
                    value = 1.0f;
                    break;
            }
            return "Bot.Sleep(" + value.ToString("0.00") + ");";
        }

        private static string SkipCode() {
            return "(*SKIP*)";
        }

        private static string ScanCode() {
            return "Bot.ScanForTreats();\nIF Bot.HasNextScan() THEN\n    Bot.GetNextScan(bearing, distance);";
        }

        private static string MoveCode(Value duration) {
            float value = 1.0f;
            switch (duration) {
                case Value.Low:
                    value = 0.03f;
                    break;
                case Value.Mid:
                    value = 0.06f;
                    break;
                case Value.High:
                    value = 0.15f;
                    break;
            }
            return "Bot.RunEngine(bearing, distance * " + value.ToString("0.000") + ");";
        }

        private static string EnergyCode(Value energyLevel) {
            float value = 1.0f;
            switch (energyLevel) {
                case Value.Low:
                    value = 1.0f / 4f;
                    break;
                case Value.Mid:
                    value = 1.0f / 2f;
                    break;
                case Value.High:
                    value = 1.0f;
                    break;
            }
            return "IF BatLevel() > " + value.ToString("0.0") + " THEN";
        }

        private static string CloneCode() {
            return "Bot.Clone();";
        }
    }
}