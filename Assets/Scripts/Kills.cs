using UnityEngine;
using UnityEngine.UI;

public class Kills : MonoBehaviour
{
    private int count = 0;

    public void updateKillText()
    {
        count++;
        gameObject.GetComponent<Text>().text = "" + count;
    }
}
