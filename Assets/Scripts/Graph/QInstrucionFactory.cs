using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Graph {
    static class QInstrucionFactory {
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

        private static string SleepCode(Value time) {
            return "Bot.Sleep(" + time + ")";
        }

        private static string SkipCode() {
            return "(*Skip this instrucion yo!*)";
        }

        private static string ScanCode() {
            return "Bot.ScanForTreats();\nIF Bot.HasNextScan() THEN";
        }

        private static string MoveCode(Value duration) {
            return "Bot.RunEngine(bearing," + duration + ");";
        }

        private static string EnergyCode(Value energyLevel) {
            return "IF GetBattery() > " + energyLevel + " THEN";
        }

        private static string CloneCode() {
            return "Bot.Clone();";
        }
    }
}