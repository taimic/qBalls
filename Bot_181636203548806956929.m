MODULE Bot_181636203548806956929;
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
(*SKIP*)
(*SKIP*)
(*SKIP*)
(*SKIP*)
(*SKIP*)
(*SKIP*)
(*SKIP*)
Bot.Sleep(0.2);
END
END Bot_181636203548806956929.