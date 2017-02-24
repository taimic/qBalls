MODULE Bot_242636235321857802728;
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
Bot.RunEngine(bearing, distance * 0.150);
ELSE
Bot.Sleep(0.33);
END
IF BatLevel() > 0.5 THEN
Bot.RunEngine(bearing, distance * 0.030);
ELSE
Bot.Clone();
END
(*SKIP*)
Bot.Clone();
Bot.Sleep(0.4);
END
END Bot_242636235321857802728.