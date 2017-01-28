MODULE Bot_251636209580737632649;
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
ELSE

END


(*SKIP*)

Bot.Sleep(0.4);
END
END Bot_251636209580737632649.