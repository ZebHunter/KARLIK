using UnityEngine;

public class EnemyKarma : MonoBehaviour
{
    [SerializeField] private int _karmaPoints = 1;
    
    public void ChangeKarma()
    {
        GameObject.Find("KarmaSystem").GetComponent<KarmaSystem>().GetKarmaChange(_karmaPoints); // NB: I THINK KARMA OBJECT HAVE TO BE NAMED WITH ONLY THIS NAME
    }
}