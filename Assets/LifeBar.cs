using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{
    public Image lifeBarBlue;
    private Player player;
    private float maxLife;
    void Start()
    {
    player = GameObject.Find("Player").GetComponent<Player>();
    maxLife = player.life;

    }
    
    void Update()
    {
        lifeBarBlue.fillAmount = player.life / maxLife;
    }
}