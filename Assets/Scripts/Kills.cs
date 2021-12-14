using UnityEngine;
using UnityEngine.UI;

public class Kills : MonoBehaviour
{
    private int count = 0;

    // update the text that shows the total number of enemies killed
    public void updateKillText()
    {
        count++;
        gameObject.GetComponent<Text>().text = "" + count;
    }
}
