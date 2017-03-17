MODULE Q_asdf_2017761013;
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
Bot.Sleep(0.4);
END
END Q_asdf_2017761013.