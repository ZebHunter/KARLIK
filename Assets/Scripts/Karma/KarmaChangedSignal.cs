using UnityEngine;
public class KarmaChangedSignal
{
    public readonly int NewEnemyLevel;
    public readonly int Karma;
    
    public KarmaChangedSignal(int karma)
    {
        Karma = karma;
        if(karma < 25) NewEnemyLevel = 0;
        if(karma >= 25 && karma < 50) NewEnemyLevel = 1;
        if(karma >= 50 && karma < 75) NewEnemyLevel = 2;
        if(karma >= 75) NewEnemyLevel = 3;
    }
}