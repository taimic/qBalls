MODULE Bot_271636211142908542879;
IMPORT Bot, Math, Random;
VAR bearing, distance, isClone:REAL;

PROCEDURE BatLevel(): REAL;
VAR max, cur: REAL;
BEGIN
    max := Bot.GetMaxBattery();
    cur:= Bot.GetBattery();
RETURN cur / max;
END BatLevel;

BEGIN
WHILE TRUE DO
Bot.Clone();
Bot.RunEngine(bearing, distance * 0.060);
Bot.ScanForTreats();
IF Bot.HasNextScan() THEN
    Bot.GetNextScan(bearing, distance);
Bot.Sleep(0.67);
ELSE
Bot.Sleep(0.33);
END
Bot.Clone();
Bot.Clone();

END
Bot.Sleep(0.4);
END
END Bot_271636211142908542879.