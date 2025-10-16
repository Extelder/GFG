using EvolveGames;
using UnityEngine;

public class PlayerTime : TimeAffectable
{
    [SerializeField] private PlayerController _playerController;

    public override void OnTimeValueChanged(float value, bool affectPlayer)
    {
        if (affectPlayer)
        {
            Time.timeScale = value;
            _playerController.SensetivityMultiplier += 1 - value;
        }
        else
        {
            Time.timeScale = 1;
            _playerController.SensetivityMultiplier = 1;
        }
    }
}