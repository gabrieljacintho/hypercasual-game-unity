using UnityEngine;

namespace Bits.ExperienceSystem
{
    public class ExperienceLevelUp : MonoBehaviour
    {
        public void LevelUp()
        {
            ExperienceManager.Instance.ExperienceLevel++;
        }
    }
}