MODULE Bot_181636203561079214934;
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
Bot.Clone();
Bot.Clone();
Bot.Clone();
Bot.Clone();
Bot.Clone();
Bot.Clone();
Bot.Clone();
Bot.Sleep(0.4);
END
END Bot_181636203561079214934.