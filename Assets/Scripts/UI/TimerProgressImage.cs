using UnityEngine;
using UnityEngine.UI;

namespace Bits
{
    [RequireComponent(typeof(Image))]
    public class TimerProgressImage : MonoBehaviour
    {
        [SerializeField] private Timer _timer;

        private Image _image;


        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void LateUpdate()
        {
            _image.fillAmount = _timer.Progress;
        }
    }
}