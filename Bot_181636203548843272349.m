MODULE Bot_181636203548843272349;
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
Bot.ScanForTreats();
IF Bot.HasNextScan() THEN
    Bot.GetNextScan(bearing, distance);
Bot.RunEngine(bearing, distance * 0.100);
ELSE
Bot.Sleep(0.33);
END
IF BatLevel() > 0.5 THEN
Bot.Clone();
ELSE
Bot.Sleep(0.67);
END
IF BatLevel() > 0.5 THEN
Bot.RunEngine(bearing, distance * 0.100);
END
Bot.Sleep(0.2);
END
END Bot_181636203548843272349.