using Bits.Patterns;

namespace Bits.ExperienceSystem
{
    public class ExperienceManager : Singleton<ExperienceManager>
    {
        private int _experienceLevel;

        public int ExperienceLevel
        {
            get => _experienceLevel;
            set => _experienceLevel = value;
        }
    }
}