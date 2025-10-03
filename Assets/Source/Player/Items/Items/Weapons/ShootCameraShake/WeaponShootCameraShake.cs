using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShootCameraShake : MonoBehaviour
{
    [SerializeField] private WeaponShoot _weaponShoot;

    private void OnEnable()
    {
        _weaponShoot.CameraShake += ShootPerformed;
    }

    private void OnDisable()
    {
        _weaponShoot.CameraShake -= ShootPerformed;
    }

    private void ShootPerformed()
    {
        /*   var cameraTransform = _camera.transform;
           _camera.DOShakePosition(0.6f, new Vector3(0.0035f, 0.0035f, 0.0035f) * 2, 10, 0, false, true,
                   ShakeRandomnessMode.Full)
               .SetEase(Ease.InOutBounce)
               .SetLink(_camera.transform.gameObject);
   
           _camera.DOShakeRotation(0.3f, new Vector3(10, 2, 0), 1, 0, true)
               .SetEase(Ease.OutExpo)
               .SetLink(_camera.transform.gameObject); */
    }
}