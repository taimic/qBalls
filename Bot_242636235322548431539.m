MODULE Bot_242636235322548431539;
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
(*SKIP*)
Bot.Clone();
Bot.Clone();
Bot.ScanForTreats();
IF Bot.HasNextScan() THEN
    Bot.GetNextScan(bearing, distance);
(*SKIP*)
ELSE
Bot.Sleep(1.00);
END
IF BatLevel() > 1.0 THEN
Bot.Clone();
END
Bot.Sleep(0.4);
END
END Bot_242636235322548431539.